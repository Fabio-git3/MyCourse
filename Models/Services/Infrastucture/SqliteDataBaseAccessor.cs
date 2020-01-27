using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace MyCourse.Models.Services.Infrastucture
{
    public class SqliteDataBaseAccessor : IDataBaseAccessor
    {
        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            var queryArgument=formattableQuery.GetArguments();
            var sqliteParameters= new List<SqliteParameter>();

            for(int i=0; i<queryArgument.Length; i++){
                var parameter=new SqliteParameter(i.ToString(),queryArgument[i]);
                sqliteParameters.Add(parameter);
                queryArgument[i]="@"+i;
            }
            string query=formattableQuery.ToString();


           using(var conn= new SqliteConnection("Data Source=Data/MyCourse.db"))
           {
               await conn.OpenAsync();
               using( var cmd =new SqliteCommand(query,conn))
               {
                   cmd.Parameters.AddRange(sqliteParameters);
                   using(var reader=await cmd.ExecuteReaderAsync())
                   {
                       DataSet dataSet=new DataSet();
                       dataSet.EnforceConstraints=false;
                       do{
                       DataTable dataTable= new DataTable();
                       dataSet.Tables.Add(dataTable);
                       dataTable.Load(reader);
                       }while(!reader.IsClosed);


                       return dataSet;

                        //while(reader.Read()){
                        //    string titolo=(string)reader["Title"];
                       // }
                   }
               }
            //conn.Dispose(); nn piu necessario scriverlo se uso lo using
           }

           
           
        }
    }
}