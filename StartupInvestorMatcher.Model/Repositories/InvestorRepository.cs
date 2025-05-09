using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using StartupInvestorMatcher.Model.Entities;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class InvestorRepository : BaseRepository
    {
        public InvestorRepository(IConfiguration configuration) : base(configuration)
        {
        }

        // Get Investor by ID
        public Investor GetInvestorById(int id)
        {
            NpgsqlConnection dbConn = null;
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM investor WHERE investor_id = @id";
                cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

                var data = GetData(dbConn, cmd);
                if (data != null && data.Read())
                {
                    return new Investor(Convert.ToInt32(data["investor_id"]))
                    {
                        NameInvestor = data["name_investor"].ToString(),
                        OverviewInvestor = data["overview_investor"].ToString(),
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

        // Get all Investors
        public List<Investor> GetInvestors()
        {
            var investors = new List<Investor>();
            NpgsqlConnection dbConn = null;
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM investor";

                var data = GetData(dbConn, cmd);
                while (data != null && data.Read())
                {
                    var i = new Investor(Convert.ToInt32(data["investor_id"]))
                    {
                        NameInvestor = data["name_investor"].ToString(),
                        OverviewInvestor = data["overview_investor"].ToString(),
                        CountryId = Convert.ToInt32(data["country_id"]),
                        IndustryId = Convert.ToInt32(data["industry_id"]),
                        InvestmentSizeId = Convert.ToInt32(data["investment_size_id"])
                    };
                    investors.Add(i);
                }

                return investors;
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Insert a new Investor
        public bool InsertInvestor(Investor i)
        {
            NpgsqlConnection dbConn = null;
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO investor (name_investor, overview_investor, country_id, industry_id, investment_size_id)
                    VALUES (@name, @overview, @country_id, @industry_id, @investment_size_id)
                ";

                cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, i.NameInvestor);
                cmd.Parameters.AddWithValue("@overview", NpgsqlDbType.Text, i.OverviewInvestor);
                cmd.Parameters.AddWithValue("@country_id", NpgsqlDbType.Integer, i.CountryId);
                cmd.Parameters.AddWithValue("@industry_id", NpgsqlDbType.Integer, i.IndustryId);
                cmd.Parameters.AddWithValue("@investment_size_id", NpgsqlDbType.Integer, i.InvestmentSizeId);

                return InsertData(dbConn, cmd);
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Update an existing Investor
        public bool UpdateInvestor(Investor i)
        {
            var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                UPDATE investor SET
                    name_investor = @name,
                    overview_investor = @overview,
                    country_id = @country_id,
                    industry_id = @industry_id,
                    investment_size_id = @investment_size_id
                WHERE investor_id = @id
            ";

            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, i.NameInvestor);
            cmd.Parameters.AddWithValue("@overview", NpgsqlDbType.Text, i.OverviewInvestor);
            cmd.Parameters.AddWithValue("@country_id", NpgsqlDbType.Integer, i.CountryId);
            cmd.Parameters.AddWithValue("@industry_id", NpgsqlDbType.Integer, i.IndustryId);
            cmd.Parameters.AddWithValue("@investment_size_id", NpgsqlDbType.Integer, i.InvestmentSizeId);
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, i.InvestorId);

            return UpdateData(dbConn, cmd);
        }

        // Delete Investor by ID
        public bool DeleteInvestor(int id)
        {
            var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "DELETE FROM investor WHERE investor_id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

            return DeleteData(dbConn, cmd);
        }
    }
}
