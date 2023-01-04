using ProjectCatch.Mons.Attacks;

namespace ProjectCatch.Mons
{
    public class MonInstance
    {
        private readonly MonBase monBase;

        public int Id => monBase.Id;
        public string Species => monBase.Species;
        
        // Instance Info
        public string Name => hasNickname ? nickname : monBase.Species;
        private bool hasNickname = false;
        private string nickname;

        public int Level { get; private set; }
        public int Exp { get; private set; }

        public AttackReference[] Attacks { get; private set; } = new AttackReference[4];

        public MonInstance(MonBase monBase, int level, string nickname = "")
        {
            this.monBase = monBase;
            Level = level;

            hasNickname = !string.IsNullOrWhiteSpace(nickname);
            this.nickname = nickname;
        }
    }
}
