namespace PlayneraTest.Scripts.Gameplay.PseudoPhysics
{
    public interface IPseudoPhysicsBody
    {
        EItemType Type { get; }
        bool IsTouchedGround { get; set; }
        bool IsActive { get; set; }
        bool IsGroundPositionDirty { get; set; }
    }
}