namespace TTADotNetCore.PizzaApi.Queries;

public class PizzaQuery
{
    public static string PizzaOrderQuery { get; } = @"SELECT po.*, p.Pizza, p.Price FROM [dbo].[Tbl_PizzaOrder] po 
													INNER JOIN Tbl_Pizza p ON p.PizzaId = po.PizzaId 
													WHERE PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";

    public static string PizzaOrderDetailQuery { get; } = @"SELECT pod.*, pe.PizzaExraName, pe.Price FROM [dbo].[Tbl_PizzaOrderDetail] pod
                                                            INNER JOIN Tbl_PizzaExtra pe ON pe.PizzaExtraId = pod.PizzaExtraId
                                                            WHERE PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";
}
