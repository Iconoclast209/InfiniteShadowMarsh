public interface ICanPatrol {
    float PatrolRadius { get; set; }
    bool IsMovingLeft { get; set; }
    float MovementSpeed { get; set; }
    float StartingPosition { get; set; }
}
