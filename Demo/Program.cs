﻿using System;
using System.Configuration;
using NPhoenix.Client;

namespace NPhoenix.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var connection = new NPhoenixConnection(ConfigurationManager.AppSettings["PhoenixZookeeper"]))
                {
                    connection.Connect();
                    var statement = connection.CreateStatement();
                    try
                    {
                        statement.ExecuteUpdate("create table HELLOPHOENIX (mykey integer not null primary key, mycolumn varchar)");
                    }
                    catch (NPhoenixSqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    statement.ExecuteUpdate("UPSERT INTO HELLOPHOENIX (mykey, mycolumn)VALUES (1, 'a')");
                    connection.Commit();

                    var preparedStatement = connection.PrepareStatement("select * from HELLOPHOENIX");
                    var resultSet = preparedStatement.ExecuteQuery();
                    while (resultSet.Next())
                    {
                        Console.WriteLine("mycolumn:{0} mykey:{1}", resultSet.GetString("mycolumn"), resultSet.GetString("mykey"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
