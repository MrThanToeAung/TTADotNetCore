using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TTADotNetCore.RestApiWithNLayer.Features.IncompatibleFood
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncompatibleFoodController : ControllerBase
    {
        private async Task<IncompatibleFood> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("IncompatibleFood.json");
            var model = JsonConvert.DeserializeObject<IncompatibleFood>(jsonStr);
            return model;
        }

        [HttpGet("IncompatibleFood")]
        public async Task<IActionResult> GetIncompatibleFood()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_IncompatibleFood);
        }

        [HttpGet("description")]
        public async Task<IActionResult> GetDescription(string description)
        {
            var model = await GetDataAsync();
            var list = model.Tbl_IncompatibleFood.Where(x => x.Description == description).ToList();
            return Ok(list);
        }

        [HttpGet("{foodA}/{foodB}")]
        public async Task<IActionResult> GetDescription(string foodA, string foodb)
        {
            var model = await GetDataAsync();
            var result = model.Tbl_IncompatibleFood.FirstOrDefault(x => x.FoodA == foodA && x.FoodB == foodb);
            return Ok(result.Description);
        }
    }

    public class IncompatibleFood
    {
        public Tbl_Incompatiblefood[] Tbl_IncompatibleFood { get; set; }
    }

    public class Tbl_Incompatiblefood
    {
        public int Id { get; set; }
        public string FoodA { get; set; }
        public string FoodB { get; set; }
        public string Description { get; set; }
    }

}
