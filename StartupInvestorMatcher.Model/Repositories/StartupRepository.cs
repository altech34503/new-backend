using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using StartupInvestorMatcher.Model.Entities;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class StartupRepository : BaseRepository
    {
        public StartupRepository(IConfiguration configuration) : base(configuration)
        {
        }

        // Get Startup by ID
        public Startup GetStartupById(int id)
        {
            NpgsqlConnection dbConn = null;
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM startup WHERE startup_id = @id";
                cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

                var data = GetData(dbConn, cmd);
                if (data != null && data.Read())
                {
                    return new Startup(Convert.ToInt32(data["startup_id"]))
                    {
                        NameStartup = data["name_startup"].ToString(),
                        OverviewStartup = data["overview_startup"].ToString(),
                        CountryId = Convert.ToInt32(data["country_id"]),
                        IndustryId = Convert.ToInt32(data["industry_id"]),
                        InvestmentSizeId = Convert.ToInt32(data["investment_size_id"])
                    };
                }
                return null;
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Get all Startups
        public List<Startup> GetStartups()
        {
            var startups = new List<Startup>();
            NpgsqlConnection dbConn = null;
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM startup";

                var data = GetData(dbConn, cmd);
                while (data != null && data.Read())
                {
                    var s = new Startup(Convert.ToInt32(data["startup_id"]))
                    {
                        NameStartup = data["name_startup"].ToString(),
                        OverviewStartup = data["overview_startup"].ToString(),
                        CountryId = Convert.ToInt32(data["country_id"]),
                        IndustryId = Convert.ToInt32(data["industry_id"]),
                        InvestmentSizeId = Convert.ToInt32(data["investment_size_id"])
                    };
                    startups.Add(s);
                }

                return startups;
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Insert new Startup
        public bool InsertStartup(Startup s)
        {
            NpgsqlConnection dbConn = null;
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO startup (name_startup, overview_startup, country_id, industry_id, investment_size_id)
                    VALUES (@name, @overview, @country_id, @industry_id, @investment_size_id)
                ";

                cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, s.NameStartup);
                cmd.Parameters.AddWithValue("@overview", NpgsqlDbType.Text, s.OverviewStartup);
                cmd.Parameters.AddWithValue("@country_id", NpgsqlDbType.Integer, s.CountryId);
                cmd.Parameters.AddWithValue("@industry_id", NpgsqlDbType.Integer, s.IndustryId);
                cmd.Parameters.AddWithValue("@investment_size_id", NpgsqlDbType.Integer, s.InvestmentSizeId);

                return InsertData(dbConn, cmd);
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Update existing Startup
        public bool UpdateStartup(Startup s)
        {
            var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                UPDATE startup SET
                    name_startup = @name,
                    overview_startup = @overview,
                    country_id = @country_id,
                    industry_id = @industry_id,
                    investment_size_id = @investment_size_id
                WHERE startup_id = @id
            ";

            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, s.NameStartup);
            cmd.Parameters.AddWithValue("@overview", NpgsqlDbType.Text, s.OverviewStartup);
            cmd.Parameters.AddWithValue("@country_id", NpgsqlDbType.Integer, s.CountryId);
            cmd.Parameters.AddWithValue("@industry_id", NpgsqlDbType.Integer, s.IndustryId);
            cmd.Parameters.AddWithValue("@investment_size_id", NpgsqlDbType.Integer, s.InvestmentSizeId);
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, s.StartupId);

            return UpdateData(dbConn, cmd);
        }

        // Delete Startup by ID
        public bool DeleteStartup(int id)
        {
            var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "DELETE FROM startup WHERE startup_id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

            return DeleteData(dbConn, cmd);
        }
    }
}
