﻿namespace Smakoowa_Api.Data.Configurations
{
    public static class InstructionConfiguration
    {
        public static void ConfigureInstruction(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instruction>().HasKey(c => c.Id);
        }
    }
}
