namespace n5.permissions.Application.Query
{
    public class GetPermissionsQuery
    {
        public int Id { get; set; }
        public GetPermissionsQuery(int id)
        {

            Id = id;
        }
        public GetPermissionsQuery()
        {
            
        }
    }
}
