using System.Collections.Generic;
using PmfBackend.Models;
using PmfBackend.Database.Entities;
using System.Threading.Tasks;
using PmfBackend.Database;
using System;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

using System.Linq;

namespace PmfBackend.Database.Repositories {
    public class AnalyticsRepository : IAnalyticsRepoository {

         private readonly TransactionDbContext _dbContext;
        public AnalyticsRepository(TransactionDbContext dbContext){
            _dbContext = dbContext;
        }
        public List<AnalyticsModel> AnalyticsByCategory(string catCode,string startDate=null,string endDate=null,string direction=null){
            
           
            var commandSql = " select \"CatCode\" as CatCode, sum(\"Amount\") as \"Amount\" , count(*) as \"Count\" from transactions where ";
           if (startDate != null){
               DateTime s = DateTime.Parse(startDate);
               commandSql += " \"Date\" > \'"+s+"\' and ";
           }
           if (endDate != null){
               DateTime e= DateTime.Parse(endDate);
               commandSql += " \"Date\" < \'"+e+"\' and ";
           }
           if (direction != null){
               commandSql += " \"Direction\" = \'"+direction+"\' and ";
           }

           commandSql += " \"CatCode\" = \'"+catCode+"\'  group by \"CatCode\" ";

           
            
            var list = _dbContext.AnalyticsModels.FromSqlRaw(commandSql).ToList();
            Console.WriteLine(list);
            return list;
        }

    }
}