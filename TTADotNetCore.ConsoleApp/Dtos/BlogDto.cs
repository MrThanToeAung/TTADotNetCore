using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTADotNetCore.ConsoleApp.Dtos;

[Table("Tbl_Blog")]
public class BlogDto
{
    [Key]
    public int BlogID { get; set; }
    public string BlogTitle { get; set; }
    public string BlogAuthor { get; set; }
    public string BlogContent { get; set; }
}

//public record BlogEntity ( int BlogId, string BlogTitle, string BlogAuthor, string BlogContent);
