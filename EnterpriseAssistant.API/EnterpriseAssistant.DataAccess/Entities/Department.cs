namespace EnterpriseAssistant.DataAccess.Entities
{
    public class Department : BaseEntity.WithId
    {
        public string Name { get; set; }

        public long? ParentDepartmentId { get; set; }
        
        public Department ParentDepartment { get; set; }
    }
}