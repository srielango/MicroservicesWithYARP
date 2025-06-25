namespace CatalogAPI.Data
{
    public class PrapareDb
    {
        public static void LoadData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!context.Products.Any())
                {
                    context.Products.AddRange(LoadProducts());
                    context.SaveChanges();
                }
            }
        }

        private static List<Product> LoadProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "HP",
                    Summary = "HP 15, 13th Gen Intel Core i5-1335U Laptop (16GB DDR4,512GB SSD) Anti-Glare, Micro-edge,15.6''/39.6cm, FHD, Win11,M365,Office24, Silver,1.59kg, Iris Xe Graphics, FHD Camera w/privacy shutter, fd0577TU",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/product01.png",
                    Price = 950.00M,
                    Category = "Laptop"
                },
                new Product()
                {
                    Id = 2,
                    Name = "Boult",
                    Summary = "Boult Newly Launched Fluid X Headphones Bluetooth Wireless with 60H Playtime, 40mm Bass Driver, Zen ENC Mic, Type-C Charging, Combat™Gaming Mode, BTv 5.4, Headphones Wireless with mic (Black)",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/product02.png",
                    Price = 840.00M,
                    Category = "Head Phone"
                },
                new Product()
                {
                    Id = 3,
                    Name = "Acer",
                    Summary = "Acer Aspire Lite,13th Gen, Intel Core i3-1305U, 16GB RAM, 512GB SSD, Full HD, 15.6\"/39.62cm, Windows 11 Home, Steel Gray, 1.59KG, AL15-53, Metal Body, 36 WHR, Thin and Light Premium Laptop.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/product03.png",
                    Price = 650.00M,
                    Category = "Laptop"
                },
                new Product()
                {
                    Id = 4,
                    Name = "Lenovo M10 HD",
                    Summary = "Lenovo Tab M10 HD 10.1” Android Tablet (32GB)",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/product04.png",
                    Price = 470.00M,
                    Category = "Tablet"
                },
                new Product()
                {
                    Id = 5,
                    Name = "boAt",
                    Summary = "boAt Rockerz 450/450R, 15 HRS Battery, 40mm Drivers, Padded Ear Cushions, Integrated Controls, Dual Modes, Bluetooth Headphones, Wireless Headphone with Mic (Luscious Black)",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/product05.png",
                    Price = 380.00M,
                    Category = "Head Phone"
                },
                new Product()
                {
                    Id = 6,
                    Name = "Lenovo",
                    Summary = "Lenovo {SmartChoice)Chromebook Intel Celeron N4500 (4GB RAM/64GB eMMC 5.1/11.6 Inch (29.46cm)/HD Display/2Wx2 Stereo Speakers/HD Camera/Chrome OS/Blue/1.21Kg), 82UY0014HA",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/product06.png",
                    Price = 240.00M,
                    Category = "Laptop"
                },
                new Product()
                {
                    Id = 7,
                    Name = "Samsung Galaxy M05",
                    Summary = "Samsung Galaxy M05 (Mint Green, 4GB RAM, 64 GB Storage) | 50MP Dual Camera | Bigger 6.7\" HD+ Display | 5000mAh Battery | 25W Fast Charging | 2 Gen OS Upgrade & 4 Year Security Update | Without Charger",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/product07.png",
                    Price = 240.00M,
                    Category = "Cell Phone"
                },
                new Product()
                {
                    Id = 8,
                    Name = "Dell Inspiron",
                    Summary = "Dell Inspiron 3530 13th Gen Laptop, Intel Core i3-1305U/8GB/512GB SSD/15.6\" (39.62cm) FHD WVA AG 120Hz 250 nits Narrow Border/Windows 11+MSO'21/McAfee 15 Month/Platinum Silver/1.62kg",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/product08.png",
                    Price = 240.00M,
                    Category = "Laptop"
                },
                new Product()
                {
                    Id = 9,
                    Name = "Kodak PixPro",
                    Summary = "KODAK PIXPRO FZ45-WH 16MP Digital Camera 4X Optical Zoom 27mm Wide Angle 1080P Full HD Video 2.7\" LCD Vlogging Camera (White)",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/product09.png",
                    Price = 240.00M,
                    Category = "Camera"
                }
            };
        }
    }
}
