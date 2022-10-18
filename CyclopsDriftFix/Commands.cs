namespace CyclopsDriftFix;

public class Commands
{
    private static TechType[] Seeds = new[]
    {
#if SN1
        "acidmushroomspore",	
        "bloodoil",	
        "bluepalmseed",	
        "purplebranchesseed",	
        "creepvinepiece",	
        "creepvineseedcluster",	
        "whitemushroomspore",	
        "eyesplantseed",	
        "redrollplantseed",	
        "gabesfeatherseed",	
        "jellyplantseed",	
        "redgreententacleseed",	
        "snakemushroomspore",	
        "kooshchunk",	
        "membraintreeseed",	
        "redbushseed",	
        "shellgrassseed",	
        "spottedleavesplantseed",	
        "spikeplantseed",	
        "purplefanseed",	
        "purplestalkseed",	
        "purpletentacleseed",	
        "bulbotreepiece",	
        "purplevegetable",	
        "fernpalmseed",	
        "orangepetalsplantseed",	
        "orangemushroomspore",	
        "hangingfruit",	
        "melonseed",	
        "purplevaseplantseed",	
        "pinkmushroomspore",	
        "purplerattlespore",	
        "pinkflowerseed",
#elif BZ
        TechType.SmallMaroonPlantSeed,
        TechType.TwistyBridgesMushroomChunk,
        TechType.PurpleBranchesSeed,
        TechType.PurpleVegetable,
        TechType.CreepvineSeedCluster,
        TechType.HeatFruit,
        TechType.FrozenRiverPlant2Seeds,
        TechType.JellyPlantSeed,
        TechType.HangingFruit,
        TechType.MelonSeed,
        TechType.SnowStalkerFruit,
        TechType.RedBushSeed,
        TechType.GenericRibbonSeed,
        TechType.KelpRootPustuleSeed,
        TechType.LeafyFruit,
        TechType.GenericSpiralChunk,
        TechType.SpottedLeavesPlantSeed,
        TechType.PurpleStalkSeed,
        TechType.DeepLilyShroomSeed,
#endif
    };

    [SMLHelper.V2.Commands.ConsoleCommandAttribute("CyclopsDriftFix_Seeds")]
    public static void CommandGiveSeeds()
    {
        
        //CraftData.AddToInventory
    }
}