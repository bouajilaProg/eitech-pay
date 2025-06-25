using Back.Models.LicenceRelated;
using System.Collections.Generic;

namespace Back.Modules.LicenceModule.Dtos
{
    public class LicenceDetailsDto
    {
        public string LicenceId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int MaxDevices { get; set; }
        public int Duration { get; set; }
        public string GracePeriod { get; set; }
        public string PublicKey { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsArchived { get; set; }

        public List<LicenceOption> Options { get; set; } = new();
    }
}
