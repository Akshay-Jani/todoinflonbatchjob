using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace UserToDoDL
{
    public static class DbHelper
    {
        const string ConnectionString = @"Data Source=DESKTOP-VEK4TQG\SQLEXPRESS;Initial Catalog=ToDoInfilon;Integrated Security=True";
        const int QueryTimeOut = 300;

        private static IDbConnection _sqlConnection;

        public static DataTable GetDataTable<T>(List<T> collection)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propCollection = TypeDescriptor.GetProperties(typeof(T));

            for (int i = 0; i < propCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }

            object[] values = new object[propCollection.Count];
            foreach (T element in collection)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propCollection[i].GetValue(element);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static async Task<T> PostAsync<T>(string procedure, object parameters)
        {
            using (IDbConnection connection = DbHelper.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<T>(
                    procedure,
                    parameters,
                    commandTimeout: QueryTimeOut,
                    commandType: CommandType.StoredProcedure);
            }
        }

        private static IDbConnection GetConnection()
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection();
                _sqlConnection.ConnectionString = ConnectionString;
                _sqlConnection.Open();
            }
            return _sqlConnection;
        }
    }
}
