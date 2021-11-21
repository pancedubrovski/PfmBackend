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
        public AnalyticsGroupModel AnalyticsByCategory(string catCode,string startDate=null,string endDate=null,Direction? direction=null){
            
           
            var commandSql = " select \"pom\".\"CatCode\",Sum(\"pom\".\"Amount\") as \"Amount\" ,Count(*) as \"Count\" " +
            "from ( select \"Id\",\"CatCode\",\"Amount\",\"Date\",\"Direction\" " +
	        "from transactions " + 
            "where  \"IsSplit\" = \'false\' " +
            "union " + 
            "select  \"TransactionId\", \"sp\".\"CatCode\", \"sp\".\"Amount\", \"tr\".\"Date\",\"tr\".\"Direction\" "+
            "from \"splitTransactions\" sp join transactions  tr "+
	        "on \"sp\".\"TransactionId\" = \"tr\".\"Id\") pom  where ";
           
           
            
       
            if (startDate != null){
               DateTime s = Convert.ToDateTime(startDate);
               commandSql += " \"Date\" > \'"+s+"\' and ";
            }
            if (endDate != null){

               DateTime e=  Convert.ToDateTime(endDate);
               commandSql += " \"Date\" < \'"+e+"\' and ";
            }
            if (direction != null){
               int enumValue = (int)direction;
               commandSql += " \"Direction\" = \'"+enumValue+"\' and ";
            }
            string commandSqllTemp = null;
            if (catCode != null){
                
                
                if(char.IsLetter(catCode[0])) {
                commandSqllTemp = commandSql;    
                 commandSql += " \"pom\".\"CatCode\" in (select \"Code\" from categories "+
						 "where \"ParentCode\" = \'"+catCode+"\') and " ;
                commandSqllTemp += " \"pom\".\"CatCode\" = \'"+catCode+"\'  and 1=1 group by \"pom\".\"CatCode\"  ";
                }
                else {
                    commandSql += " \"pom\".\"CatCode\" = \'"+catCode+"\'  and ";
                }
            }
           
             commandSql +=  " 1=1 group by \"pom\".\"CatCode\" ";
             List<AnalyticsModel> list  = new List<AnalyticsModel>();
            if (commandSqllTemp != null){
                list.AddRange(_dbContext.AnalyticsModels.FromSqlRaw(commandSqllTemp).ToList());
            }
            
             list.AddRange(_dbContext.AnalyticsModels.FromSqlRaw(commandSql).ToList());
            
            return new AnalyticsGroupModel {
                Groups = list
            };
        }

    }
}