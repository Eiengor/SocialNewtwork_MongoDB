using Neo4j.Driver;

namespace DAL_Neo4j
{
    public class Neo4JCommands
    {
        readonly IDriver driver;
        public Neo4JCommands(string URI, string USER, string PASSWORD)
        {
            driver = GraphDatabase.Driver(URI, AuthTokens.Basic(USER, PASSWORD));
        }

        async public Task CreateNode(string COLLECTION, Dictionary<string, object> props)
        {
            await using (var session = driver.AsyncSession())
            {
                await session.ExecuteWriteAsync(async t =>
                {
                    var cmd = $"create (a:{COLLECTION} $properties)";
                    await t.RunAsync(cmd, new { properties = props });
                });
            }
        }

        async public Task CreateRelationship(string COLLECTION1, string COLLECTION2, string PROPERTY1, string PROPERTY2, string VALUE1, string VALUE2, string RELATIONSHIP)
        {
            await using (var session = driver.AsyncSession())
            {
                await session.ExecuteWriteAsync(async t =>
                {
                    var cmd = $@"match (a:{COLLECTION1} {{{PROPERTY1}:$value1}}), (b:{COLLECTION2} {{{PROPERTY2}:$value2}}) create (a)-[:{RELATIONSHIP}]->(b)";
                    await t.RunAsync(cmd, new { value1 = VALUE1, value2 = VALUE2 });
                });
            }
        }

        async public Task DetachDelete(string COLLECTION, string PROPERTY, string VALUE)
        {
            await using (var session = driver.AsyncSession())
            {
                await session.ExecuteWriteAsync(async t =>
                {
                    var cmd = $"match (a:{COLLECTION} {{{PROPERTY}:$value}}) detach delete a";
                    await t.RunAsync(cmd, new { value = VALUE });
                });
            }
        }

        async public Task DeleteRelationship(string COLLECTION1, string COLLECTION2, string PROPERTY1, string PROPERTY2, string VALUE1, string VALUE2, string RELATIONSHIP)
        {
            await using (var session = driver.AsyncSession())
            {
                await session.ExecuteWriteAsync(async t =>
                {
                    var cmd = $@"match (:{COLLECTION1} {{{PROPERTY1}:$value1}}) -[r:{RELATIONSHIP}]-> (:{COLLECTION2} {{{PROPERTY2}:$value2}}) delete r";
                    await t.RunAsync(cmd, new { value1 = VALUE1, value2 = VALUE2 });
                });
            }
        }

        async public Task<int> ShortestPath(string COLLECTION1, string COLLECTION2, string PROPERTY1, string PROPERTY2, string VALUE1, string VALUE2, string RELATIONSHIP, int MAXDEPTH = 3)
        {
            int pathLength = 0;
            await using (var session = driver.AsyncSession())
            {
                await session.ExecuteWriteAsync(async t =>
                {
                    var cmd = $@"match (a:{COLLECTION1} {{{PROPERTY1}:$value1}}), (b:{COLLECTION2} {{{PROPERTY2}:$value2}}), l = shortestPath((a)-[:{RELATIONSHIP} *1..{MAXDEPTH}]->(b)) return length(l) as length";
                    var queryRes = await t.RunAsync(cmd, new { value1 = VALUE1, value2 = VALUE2 });
                    var singleRecord = await queryRes.SingleAsync();
                    pathLength = singleRecord["length"].As<int>();
                });
            }
            return pathLength;
        }
    }
}
