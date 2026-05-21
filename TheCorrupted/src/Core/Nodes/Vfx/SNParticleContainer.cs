using Godot;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;

namespace TheCorrupted.TheCorrupted.src.Core.Nodes.Vfx
{
    public partial class SNParticleContainer : NParticlesContainer
    {
        public override void _Ready()
        {
            // 1. Create a new Godot Array to hold the particles
            var particlesArray = new Godot.Collections.Array<GpuParticles2D>();

            // 2. Loop through all child nodes attached to this container
            foreach (Node child in GetChildren())
            {
                // If the child is a particle system, add it to our array
                if (child is GpuParticles2D gpuParticle)
                {
                    particlesArray.Add(gpuParticle);
                }
            }

            // 3. Forcefully set the base game's private variable using Godot's reflection
            Set("_particles", particlesArray);
        }
    }
}