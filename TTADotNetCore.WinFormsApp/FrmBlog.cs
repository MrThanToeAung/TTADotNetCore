using TTADotNetCore.Shared;
using TTADotNetCore.WinFormsApp.Models;
using TTADotNetCore.WinFormsApp.Queries;

namespace TTADotNetCore.WinFormsApp;

public partial class FrmBlog : Form
{

    private readonly DapperService _dapperService;
    public FrmBlog()
    {
        InitializeComponent();
        _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BlogModel blog = new BlogModel();
            blog.BlogTitle = txtTitle.Text.Trim();
            blog.BlogAuthor = txtAuthor.Text.Trim();
            blog.BlogContent = txtContent.Text.Trim();

            int result = _dapperService.Execute(BlogQuery.blogCreate, blog);
            string message = result > 0 ? "Saving Successful! " : "Saving Fail";
            MessageBox.Show(message,
                "SaveBlog",
                MessageBoxButtons.OK,
                result> 0 ?  MessageBoxIcon.Information : MessageBoxIcon.Error);

            if(result > 0) ClearControl();

        }catch(Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    private void ClearControl()
    {
        txtTitle.Clear();
        txtAuthor.Clear();
        txtContent.Clear();
        txtTitle.Focus();
    }
}