using AutoMapper;
using PmfBackend.Commands;
using PmfBackend.Database.Entities;
using System.Collections.Generic;
using PmfBackend.Models;
using System;
using System.Globalization;

namespace PmfBackend.Mappings {
    public class AutoMapperProfile : Profile {
        
        public AutoMapperProfile() {
            
            CreateMap<CreateTransactionCommand,TransactionEntity>()
            .ForMember(d => d.Date, opt =>  opt.MapFrom(x=> DateTime.ParseExact(converDateFromString(x.Date),"MM/dd/yyyy",null)));
            CreateMap<List<CreateTransactionCommand>, List<TransactionEntity>>();
            CreateMap<PagedSortedList<TransactionEntity>, PagedSortedList<Transaction>>();


            CreateMap<List<CreateCategoryCommand>, List<CategoryEntity>>();


        }
        public string converDateFromString(string input){
           
           
            string[] dateString = input.Split('/');
           
            if (dateString.Length != 3 ){
                return dateString.ToString();
            }
            if (dateString[0].Length == 1 && dateString[1].Length == 1){
                return '0'+dateString[0]+"/"+'0'+dateString[1]+"/"+dateString[2];
            }
            else if (dateString[0].Length == 1) {  
                return ('0'+dateString[0]+"/"+dateString[1]+"/"+dateString[2]);
            }
            else if (dateString[1].Length == 1){
                return (dateString[0]+"/"+'0'+dateString[1]+"/"+dateString[2]);
            }
            
            
            return (dateString[0]+"/"+dateString[1]+"/"+dateString[2]);
                  
            
           

        }

    }
}