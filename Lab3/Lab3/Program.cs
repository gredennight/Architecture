public interface IQueryBuilder
{
    IQueryBuilder Select(string columns, string table);
    IQueryBuilder Where(string conditions);
    IQueryBuilder Limit(int limit);
    string GetSQL();
}

public class Director
{
    private IQueryBuilder builder;

    public Director(IQueryBuilder builder)
    {
        this.builder = builder;
    }

    public string ConstructQuery(string columns, string table, string conditions, int limit)
    {
        return builder
            .Select(columns, table)
            .Where(conditions)
            .Limit(limit)
            .GetSQL();
    }
}

public class MySQLBuilder : IQueryBuilder
{
    private string query;

    public MySQLBuilder()
    {
        query = new string("");
    }

    public IQueryBuilder Select(string columns, string table)
    {
        query = query + "SELECT " + columns + " FROM " + table;
        return this;
    }

    public IQueryBuilder Where(string conditions)
    {
        query = query + " WHERE " + conditions;
        return this;
    }

    public IQueryBuilder Limit(int limit)
    {
        query = query + " LIMIT " + limit;
        return this;
    }

    public string GetSQL() { return query + ";"; }
}

public class PostgreSQLBuilder : IQueryBuilder
{
    private string query;

    public PostgreSQLBuilder()
    {
        query = new string("");
    }

    public IQueryBuilder Select(string columns, string table)
    {
        query = query + "SELECT " + columns + " FROM " + table;
        return this;
    }

    public IQueryBuilder Where(string conditions)
    {
        query = query + " WHERE " + conditions;
        return this;
    }

    public IQueryBuilder Limit(int limit)
    {
        query = query + " LIMIT " + limit;
        return this;
    }

    public string GetSQL() { return query + ";"; }
}

public class Program
{
    public static void Main(string[] args)
    {
        IQueryBuilder mySQLBuilder = new MySQLBuilder();
        Director mysqlDirector = new Director(mySQLBuilder);
        string mySQLQuery = mysqlDirector.ConstructQuery("id, name, email", "users", "age > 18 AND status = 'active'", 10);
        Console.WriteLine("MySQL query:\n------------------------------------------------------------------------------\n" + mySQLQuery);

        IQueryBuilder postgreSQLBuilder = new PostgreSQLBuilder();
        Director postgresDirector = new Director(postgreSQLBuilder);
        string postgreSQLQuery = postgresDirector.ConstructQuery("id, name, email", "users", "age > 18 AND status = 'active'", 10);
        Console.WriteLine("\n\nPostgreSQL query:\n------------------------------------------------------------------------------\n" + postgreSQLQuery);
    }
}
