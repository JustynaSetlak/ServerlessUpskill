using System;

namespace UpskillStore.Utils.Wrappers
{
    public class GuidGenerator : IGuidGenerator
    {
        public Guid GenerateGuid() => Guid.NewGuid();
    }
}
