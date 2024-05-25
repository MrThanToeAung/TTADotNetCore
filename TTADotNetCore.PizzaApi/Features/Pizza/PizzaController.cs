using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTADotNetCore.PizzaApi.Database;
using TTADotNetCore.PizzaApi.Queries;
using TTADotNetCore.RestApi;
using TTADotNetCore.Shared;

namespace TTADotNetCore.PizzaApi.Features.Pizza;

[Route("api/[controller]")]
[ApiController]
public class PizzaController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly DapperService _dapperService;

    public PizzaController()
    {
        _appDbContext = new AppDbContext();
        _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }

    [HttpGet]
    public async Task<IActionResult> GetPizzaAsync()
    {
        var pizzaList = await _appDbContext.Pizzas.ToListAsync();
        return Ok(pizzaList);
    }

    [HttpGet("Extras")]
    public async Task<IActionResult> GetPizzaExtraAsync()
    {
        var pizzaExtraList = await _appDbContext.PizzaExtras.ToListAsync();
        return Ok(pizzaExtraList);
    }

    //[HttpGet("Order/{invoiceNo}")]
    //public async Task<IActionResult> GetOrderInvoiceAsync(string invoiceNo)
    //{
    //    var item = await _appDbContext.PizzaOrders.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNo);
    //    var lst = await _appDbContext.PizzaOrderDetails.Where(x => x.PizzaOrderInvoiceNo == invoiceNo).ToListAsync();

    //    return Ok(new
    //    {
    //        Order = item,
    //        OrderDetail = lst

    //    });
    //}

    // API using Query with Dapper Service
    [HttpGet("Order/{invoiceNo}")]
    public IActionResult GetOrder(string invoiceNo)
    {
        var item = _dapperService.QueryFirstOrDefault<PizzaOrderInvoiceHeadModel>
            (
                PizzaQuery.PizzaOrderQuery,
                new { PizzaOrderInvoiceNo = invoiceNo }
            );

        var lst = _dapperService.Query<PizzaOrderInvoiceDetailModel>
            (
                PizzaQuery.PizzaOrderDetailQuery,
                new { PizzaOrderInvoiceNo = invoiceNo }
            );

        var model = new PizzaOrderInvoiceResponse
        {
            Order = item,
            OrderDetail = lst
        };

        return Ok(model);
    }

    [HttpPost("Order")]
    public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
    {
        var pizzaItem = await _appDbContext.Pizzas.FirstOrDefaultAsync(x => x.ID == orderRequest.PizzaId);
        var totalPrice = pizzaItem.Price;

        if (orderRequest.Extras.Length > 0)
        {
            // select * from Tbl_PizzaExtra where PizzaExtraId in (1, 2, 3, 4, 5);
            //foreach(var item in  orderRequest.Extras) { }

            var extraItemsList = await _appDbContext.PizzaExtras.Where(x => orderRequest.Extras.Contains(x.ID)).ToListAsync();
            totalPrice += extraItemsList.Sum(x => x.Price);
        }

        var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
        PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
        {
            PizzaId = orderRequest.PizzaId,
            PizzaOrderInvoiceNo = invoiceNo,
            TotalAmount = totalPrice
        };

        List<PizzaOrderDetailModel> pizzaOrderDetailModels = orderRequest.Extras.Select(extraId => new PizzaOrderDetailModel()
        {
            PizzaExtraId = extraId,
            PizzaOrderInvoiceNo = invoiceNo
        }).ToList();

        await _appDbContext.PizzaOrders.AddAsync(pizzaOrderModel);
        await _appDbContext.PizzaOrderDetails.AddRangeAsync(pizzaOrderDetailModels);
        await _appDbContext.SaveChangesAsync();

        OrderResponse response = new OrderResponse()
        {
            InvoiceNo = invoiceNo,
            Message = "Thank you for your order! Enjoy your pizza!",
            TotalAmount = totalPrice,
        };

        return Ok(response);
    }    
}


