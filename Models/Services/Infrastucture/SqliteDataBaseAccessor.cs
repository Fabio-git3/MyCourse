using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ValueTypes;

namespace MyCourse.Models.Services.Infrastucture
{
    public class SqliteDataBaseAccessor : IDataBaseAccessor
    {
        private readonly ILogger<AdoNetCourseService> logger;
        private readonly IOptionsMonitor<ConnectionStringsOptions> connectionStringsOptions;

        public SqliteDataBaseAccessor(ILogger<AdoNetCourseService> logger,IOptionsMonitor<ConnectionStringsOptions> connectionStringsOptions)
        {
            this.logger = logger;
            this.connectionStringsOptions = connectionStringsOptions;
        }

       

        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            logger.LogInformation(formattableQuery.Format,formattableQuery.GetArguments());
            
            var queryArgument=formattableQuery.GetArguments();
            var sqliteParameters= new List<SqliteParameter>();

            for(int i=0; i<queryArgument.Length; i++){
                if(queryArgument[i] is Sql){
                    continue;
                }
                var parameter=new SqliteParameter(i.ToString(),queryArgument[i]);
                sqliteParameters.Add(parameter);
                queryArgument[i]="@"+i;
            }
            string query=formattableQuery.ToString();

            string connectionString= connectionStringsOptions.CurrentValue.Default;
           using(var conn= new SqliteConnection(connectionString))
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