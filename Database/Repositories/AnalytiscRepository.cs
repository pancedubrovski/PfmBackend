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
            
           
            var commandSql = " select \"pom\".\"catcode\",Sum(\"pom\".\"amount\") as \"Amount\" ,Count(*) as \"Count\" " +
            "from ( select \"id\",\"catcode\",\"amount\",\"date\",\"direction\" " +
	        "from transactions " + 
            "where  \"issplit\" = \'false\' " +
            "union " + 
            "select  \"TransactionId\", \"sp\".\"CatCode\", \"sp\".\"Amount\", \"tr\".\"date\",\"tr\".\"direction\" "+
            "from \"splitTransactions\" sp join transactions  tr "+
	        "on \"sp\".\"TransactionId\" = \"tr\".\"id\") pom  where ";
           
           
            
       
            if (startDate != null){
               DateTime s = Convert.ToDateTime(startDate);
               commandSql += " \"date\" > \'"+s+"\' and ";
            }
            if (endDate != null){

               DateTime e=  Convert.ToDateTime(endDate);
               commandSql += " \"date\" < \'"+e+"\' and ";
            }
            if (direction != null){
               int enumValue = (int)direction;
               commandSql += " \"direction\" = \'"+enumValue+"\' and ";
            }
            string commandSqllTemp = null;
            if (catCode != null){
                
                
                if(char.IsLetter(catCode[0])) {
                commandSqllTemp = commandSql;    
                 commandSql += " \"pom\".\"catcode\" in (select \"Code\" from categories "+
						 "where \"ParentCode\" = \'"+catCode+"\') and " ;
                commandSqllTemp += " \"pom\".\"catcode\" = \'"+catCode+"\'  and 1=1 group by \"pom\".\"catcode\"  ";
                }
                else {
                    commandSql += " \"pom\".\"catcode\" = \'"+catCode+"\'  and ";
                }
            }
           
             commandSql +=  " 1=1 group by \"pom\".\"catcode\" ";
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