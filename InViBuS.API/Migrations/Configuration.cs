using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using InViBuS.API.Models;

namespace InViBuS.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InViBuSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        private static void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
        }

        protected override void Seed(InViBuSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            base.Seed(context);
            context.Users.AddOrUpdate(u => u.UserName,
                new User
                {
                    Company = "MOE",
                    UserName = "moetest",
                    Password = "test1234",
                    Email = "test@moe.dk",
                    FirstName = "Test",
                    LastName = "Testesen"
                },
                new User
                {
                    Company = "MOE",
                    UserName = "test2222",
                    Password = "testpw",
                    Email = "test@test.dk",
                    FirstName = "MoeTest",
                    LastName = "Moe"
                });
            SaveChanges(context);
            var firstOrDefault = context.Users.FirstOrDefault(u => u.UserName == "moetest");
            if (firstOrDefault != null)
                context.Projects.AddOrUpdate(p => p.ProjectName,
                    new Project
                    {
                        ProjectName = "TestProject",
                        Description = "This is a test project",
                        StartDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                        LastUploadedDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                        //AnalysisMetadata = new List<AnalysisMetadata> {
                        //    new AnalysisMetadata {
                        //        Subject = "Test data 2",
                        //        DescriptionLong = "Test2.",
                        //        NumSim = 10000,
                        //        NumIn = 30,
                        //        NumOut = 5,
                        //        Iteration = 5,
                        //        Conclusion = "Test doesn't work!",
                        //        SourceFile = "blah.json",
                        //        TimeSim = 30,
                        //        UploadDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                        //        Filters = new List<Filter>
                        //        {
                        //            new Filter
                        //            {
                        //                MaxVal = 42.574,
                        //                MinVal = 2.546,
                        //                NumFilter = 1
                        //            }
                        //        },
                        //        AnalysisData = new AnalysisData
                        //        {
                        //            AnalysisDataJson = "NOPE"
                        //        }
                        //    }
                        //},
                        SourceDatas = new List<SourceData>
                        {
                            new SourceData
                            {
                                SourceDataBLOB = Encoding.UTF8.GetBytes("1")
                            }
                        },
                        IdProjectManager = context.Users.Find(1).UserId,
                        ProjectManager = context.Users.Find(1).UserName,
                        Users = context.Users.ToList()
                    });
            SaveChanges(context);
            if (firstOrDefault != null)
                context.Projects.AddOrUpdate(p => p.ProjectName,
                    new Project
                    {
                        ProjectName = "Test Project 2",
                        Description = "This is also a test project",
                        StartDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                        LastUploadedDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                        SourceDatas = new List<SourceData>
                        {
                            new SourceData
                            {
                                SourceDataBLOB = Encoding.UTF8.GetBytes("2")
                            }
                        },
                        IdProjectManager = context.Users.Find(2).UserId,
                        ProjectManager = context.Users.Find(2).UserName,
                        Users = context.Users.ToList()
                    });
            context.AnalysisMetadatas.AddOrUpdate(amd => amd.Subject,
                new AnalysisMetadata
                {
                    Subject = "Test data",
                    DescriptionLong = "Test Test Test Test Test Test Test Test Test Test.",
                    NumSim = 10000,
                    NumIn = 30,
                    NumOut = 5,
                    Iteration = 5,
                    Conclusion = "Test works!",
                    SourceFile = "smthg, smthg, smthg.json",
                    TimeSim = 30,
                    UploadDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    Filters = new List<Filter>
                    {
                        new Filter
                        {
                            MaxVal = 42.574,
                            MinVal = 2.546,
                            NumFilter = 1
                        }
                    },
                    AnalysisData = new AnalysisData
                    {
                        AnalysisDataJson = "Not"
                    }
                });
            SaveChanges(context);
        }
    }
}
