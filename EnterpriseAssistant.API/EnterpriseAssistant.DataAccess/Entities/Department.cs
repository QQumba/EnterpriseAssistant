namespace ResourceBasedAuthenticationTest.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }

        public long? ParentDepartmentId { get; set; }
        
        public Department ParentDepartment { get; set; }
    }
}