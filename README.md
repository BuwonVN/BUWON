# BWERP
Develop ERP web for Buwon Vina using Blazor Server .NET 6.0

## Techstack
- ASP.NET 6.0
- REST API
- SQL Server
- Visual studio 2022
- Entity Framework Core
- Blazor Server

## How to run
- Clone project from Github
- Setup database connection in project BWERP.Api --> appsettings.json
- Setup startup project is multiple projects
- Database will be created automatically when run API project.

## References
- Microsoft Docs: https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/handle-errors?view=aspnetcore-6.0
- Blazor University: https://blazor-university.com/

## Build CRUD with Dapper
public class BugDataAccessLayer
{
    public IConfiguration Configuration;
    private const string BUGTRACKER_DATABASE = "BugTrackerDatabase";
    private const string SELECT_BUG = "select * from bugs";
    public BugDataAccessLayer(IConfiguration configuration)
    {
       Configuration = configuration; //Inject configuration to access Connection string from appsettings.json.
    }

    public async Task<List<Bug>> GetBugsAsync()
    {
       using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString(BUGTRACKER_DATABASE)))
       {
           db.Open();
           IEnumerable<Bug> result = await db.QueryAsync<Bug>(SELECT_BUG);
           return result.ToList();
       }
    }

    public async Task<int> GetBugCountAsync()
    {
       using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString(BUGTRACKER_DATABASE)))
       {
            db.Open();
            int result = await db.ExecuteScalarAsync<int>("select count(*) from bugs");
            return result;
       }
    }

    public async Task AddBugAsync(Bug bug)
    {
       using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString(BUGTRACKER_DATABASE)))
       {
            db.Open();
            await db.ExecuteAsync("insert into bugs (Summary, BugPriority, Assignee, BugStatus) values (@Summary, @BugPriority, @Assignee, @BugStatus)", bug);
       }
    }

    public async Task UpdateBugAsync(Bug bug)
    {
       using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString(BUGTRACKER_DATABASE)))
       {
            db.Open();
            await db.ExecuteAsync("update bugs set Summary=@Summary, BugPriority=@BugPriority, Assignee=@Assignee, BugStatus=@BugStatus where id=@Id", bug);
       }
    }

    public async Task RemoveBugAsync(int bugid)
    {
       using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString(BUGTRACKER_DATABASE)))
       {
            db.Open();
            await db.ExecuteAsync("delete from bugs Where id=@BugId", new { BugId = bugid });
       }
    }
}

