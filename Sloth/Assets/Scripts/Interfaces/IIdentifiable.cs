/// <summary>Interface used by all objects that are identifiable via mouse hover.</summary>
public interface IIdentifiable {
    string Name { get; set; }
    string Description { get; set; }
    void Identify();
    void RemoveIdentity();
}
