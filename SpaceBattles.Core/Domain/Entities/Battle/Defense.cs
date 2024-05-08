namespace SpaceBattles.Core.Domain.Entities.Battle;

using SpaceBattles.Core.Domain.Records;

public sealed class Defense : CombatEntity
{
    public static Defense[] Defenses()
    {
        return
        [
            new Defense
            {
                Id = 1,
                ImageName = "rocket_launcher.webp",
                Name = "Rocket launcher",
                Description = "The rocket launcher is a simple, cost-effective defensive option. It is simply a development of common ballistic firearms and requires no further research.\nIts limited manufacturing costs justify its use against smaller fleets, however it loses significance over the course of time. Later it is used only as a backstop behind larger cannons.",

                BaseArmor = 200,
                BaseShield = 20,
                BaseWeaponPower = 80,

                TitaniumCost = 2_000,

                BuildingRequirements =
                [
                    new BuildingRequirement(8, 1),
                ],
            },
            new Defense
            {
                Id = 2,
                Name = "Light laser",
                ImageName = "light_laser.webp",
                Description = "To keep pace with the ever increasing speed of development in terms of spacecraft technology, scientists had to come up with a new kind of defence system able to deal with stronger and better equipped ships and her firepower of modern ships.\nBecause a low price of the unit was an essential design goal, the base structure has not been improved compared to the missile launcher.\r\n\r\nBecause the light laser offers most bang for the buck, it is the best known defence system as it is used by small, emerging empires and large multi-galactic empires equally.",

                BaseArmor = 200,
                BaseShield = 25,
                BaseWeaponPower = 100,

                TitaniumCost = 1_500,
                SiliconCost = 500,

                BuildingRequirements =
                [
                    new BuildingRequirement(8, 2),
                ],
            },
            new Defense
            {
                Id = 3,
                Name = "Heavy laser",
                ImageName = "heavy_laser.webp",
                Description = "The Heavy Laser is a practical, improved version of the Light Laser. Being more balanced than the Light Laser with improved alloy composition, it utilizes stronger, more densely packed beams, and even better onboard targeting systems.",

                BaseArmor = 800,
                BaseShield = 100,
                BaseWeaponPower = 250,

                TitaniumCost = 6_000,
                SiliconCost = 2_000,

                BuildingRequirements =
                [
                    new BuildingRequirement(8, 4),
                ],
            },
            new Defense
            {
                Id = 4,
                Name = "Ion cannon",
                ImageName = "ion_cannon.webp",
                Description = "Sometime during the 21st century, a technology known as \"electromagnetic pulse\" (contracted to EMP) was developed. These pulses prove to be very effective against computer systems and anything that possesses an electronic circuit within its structural composition.\nIn the days of its inception, this breed of weapon was deployed using rockets, missiles and bombs. However, the recent improvement of various technologies has allowed such weapons to be employed through the use of simple cannons; both static and mounted on intergalactic ships.\nSuch a focused ion beam is capable of obliterating any unshielded electrical system and destabilizes the shield circuits within its target. These combined effects often cause the destruction of the target ship while sparing any biological cargo, including the crew members.",

                BaseArmor = 800,
                BaseShield = 500,
                BaseWeaponPower = 150,

                TitaniumCost = 5_000,
                SiliconCost = 3_000,

                BuildingRequirements =
                [
                    new BuildingRequirement(8, 4),
                ],
            },
            new Defense
            {
                Id = 5,
                Name = "Gauss cannon",
                ImageName = "gauss_cannon.webp",
                Description = "Far from being a science-fiction \"weapon of tomorrow,\" the concept of a weapon using an electromagnetic impulse for propulsion originated as far back as the mid-to-late 1930s.\nBasically, the Gauss Cannon consists of a system of powerful electromagnets which fires a projectile by accelerating between a number of metal rails. Gauss Cannons fire high-density metal projectiles at extremely high velocity.\nThis weapon is so powerful when fired that it creates a sonic boom which is heard for miles, and the crew near the weapon must take special precautions due to the massive concussion effects generated.",

                BaseArmor = 3_500,
                BaseShield = 200,
                BaseWeaponPower = 1_100,

                TitaniumCost = 20_000,
                SiliconCost = 15_000,
                HeliumCost = 2_000,

                BuildingRequirements =
                [
                    new BuildingRequirement(8, 6),
                ],
            },
            new Defense
            {
                Id = 6,
                Name = "Plasma turret",
                ImageName = "plasma_turret.webp",
                Description = "One of the most advanced defense weapons systems ever developed, the Plasma Turret uses a large nuclear reactor fuel cell to power an electromagnetic accelerator that fires a pulse, or toroid, of plasma. During operation, the Plasma turret first locks in on a target and begins the process of firing.\nA plasma sphere is created in the turrets core by super heating and compressing gases, stripping them of their electrons to form ions. Once the gas is superheated, compressed, and a plasma sphere is created, it is then loaded into the electromagnetic accelerator which is then energized. Once fully energized, the accelerator is then activated, which results in the plasma sphere being launched at an extremely high rate of speed to the intended target.\nFrom your targets perspective, the approaching bluish ball of plasma is impressive, but once it strikes, it causes instant destruction.",

                BaseArmor = 10_000,
                BaseShield = 300,
                BaseWeaponPower = 3_000,

                TitaniumCost = 50_000,
                SiliconCost = 50_000,
                HeliumCost = 30_000,

                BuildingRequirements =
                [
                    new BuildingRequirement(8, 8),
                ],
            },
            new Defense
            {
                Id = 7,
                Name = "Small shield dome",
                ImageName = "small_shield_dome.webp",
                Description = "Long before shield generators became integrated and portable, there were big old generators on the surface of planets. Those were able to span a huge shielding dome around the surface of the entire planet capable of absorbing huge amounts of energy when fired upon.\nEvery now and then a smaller combat convoy is turned down by these shield domes. Using more advanced shield technology, these domes can be further increased, so their ability to absorb energy is even bigger. Only one of each shield domes can be built on a planet of course.",

                BaseArmor = 20_000,
                BaseShield = 2_000,
                BaseWeaponPower = 0,

                TitaniumCost = 10_000,
                SiliconCost = 10_000,

                BuildingRequirements =
                [
                    new BuildingRequirement(8, 1),
                ],
            },
            new Defense
            {
                Id = 8,
                Name = "Planetary shield dome",
                ImageName = "large_shield_dome.webp",
                Description = "The further development of the small shield dome. It is based on the same technology, but uses significantly more energy to withstand enemy attacks.",

                BaseArmor = 100_000,
                BaseShield = 10_000,
                BaseWeaponPower = 0,

                TitaniumCost = 50_000,
                SiliconCost = 50_000,

                BuildingRequirements =
                [
                    new BuildingRequirement(8, 6),
                ],
            }
        ];
    }
}