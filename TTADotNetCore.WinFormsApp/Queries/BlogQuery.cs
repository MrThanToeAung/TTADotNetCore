namespace TTADotNetCore.WinFormsApp.Queries;

public class BlogQuery
{
    public static string blogCreate { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
                                                    ([BlogTitle],
                                                    [BlogAuthor],
                                                    [BlogContent])
                                               VALUES
                                                    (@BlogTitle, 
                                                    @BlogAuthor,
		                                            @BlogContent)";
}
