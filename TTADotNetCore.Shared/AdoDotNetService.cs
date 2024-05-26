using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace TTADotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        //public List<T> Query<T>(string query, AdoDotNetParameter[]? parameters = null)
        public List<T> Query<T>(string query, params AdoDotNetParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                //foreach(var item in parameters)
                //{
                //    cmd.Parameters.AddWithValue(item.Name,item.Value);

                //}

                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name,item.Value)).ToArray());

                var parameterArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parameterArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            string json = JsonConvert.SerializeObject(dt); //C# >> Json
            List<T> dataList = JsonConvert.DeserializeObject<List<T>>(json)!; //Json >> C#

            return dataList;
        }
        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                //foreach(var item in parameters)
                //{
                //    cmd.Parameters.AddWithValue(item.Name,item.Value);

                //}

                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name,item.Value)).ToArray());

                var parameterArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parameterArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();
            string json = JsonConvert.SerializeObject(dt); //C# >> Json
            List<T> dataList = JsonConvert.DeserializeObject<List<T>>(json)!; //Json >> C#

            return dataList[0];
        }

        public int Execute(string query, params AdoDotNetParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
            }
            var result = cmd.ExecuteNonQuery();
            connection.Close();

            return result;
        }
    }

    public class AdoDotNetParameter
    {
        public AdoDotNetParameter() { }
        public AdoDotNetParameter(string? name, object? value)
        {
            Name = name;
            Value = value;
        }
        public string? Name { get; set; }
        public object? Value { get; set; }
    }

}
