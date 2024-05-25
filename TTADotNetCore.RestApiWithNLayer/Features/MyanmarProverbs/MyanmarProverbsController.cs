﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TTADotNetCore.RestApiWithNLayer.Features.MyanmarProverbs;

[Route("api/[controller]")]
[ApiController]
public class MyanmarProverbsController : ControllerBase
{
    private async Task<Tbl_MyanmarProverbs> GetDataFromApi()
    {
        //HttpClient client = new HttpClient();
        //var response = await client.GetAsync("https://raw.githubusercontent.com/sannlynnhtun-coding/Myanmar-Proverbs/main/MyanmarProverbs.json");
        //if (!response.IsSuccessStatusCode) return null;
        //string jsonStr = await response.Content.ReadAsStringAsync();
        //var model = JsonConvert.DeserializeObject<Tbl_MyanmarProverbs>(jsonStr);
        //return model;

        var jsonStr = await System.IO.File.ReadAllTextAsync("MyanmarProverbs.json");
        var model = JsonConvert.DeserializeObject<Tbl_MyanmarProverbs>(jsonStr);
        return model!;
    }

    [HttpGet]
    public async Task<IActionResult> GetProverb()
    {
        var model = await GetDataFromApi();
        return Ok(model.Tbl_MMProverbsTitle);
    }

    [HttpGet("{titleName}")]
    public async Task<IActionResult> GetProverbHead(string titleName)
    {
        var model = await GetDataFromApi();
        var item = model.Tbl_MMProverbsTitle.FirstOrDefault(x => x.TitleName == titleName);
        if (item == null) return NotFound();

        var titleId = item.TitleId;
        var result = model.Tbl_MMProverbs.Where(x => x.TitleId == titleId).ToList();

        List<Tbl_MmproverbsHead> lst = result.Select(x => new Tbl_MmproverbsHead
        {
            ProverbId = x.ProverbId,
            ProverbName = x.ProverbName,
            TitleId = x.TitleId
        }).ToList();

        return Ok(lst);
    }

    [HttpGet("{titleId}/{proverbId}")]
    public async Task<IActionResult> GetProverbDetail(int titleId, int proverbId)
    {
        var model = await GetDataFromApi();
        var item = model.Tbl_MMProverbs.FirstOrDefault(x => x.TitleId == titleId && x.ProverbId == proverbId);

        return Ok(item);
    }
}

public class Tbl_MyanmarProverbs
{
    public Tbl_MmproverbsTitle[] Tbl_MMProverbsTitle { get; set; }
    public Tbl_MmproverbsDetail[] Tbl_MMProverbs { get; set; }
}

public class Tbl_MmproverbsTitle
{
    public int TitleId { get; set; }
    public string TitleName { get; set; }
}

public class Tbl_MmproverbsDetail
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
    public string ProverbDesp { get; set; }
}

public class Tbl_MmproverbsHead
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
}

