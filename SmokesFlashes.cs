datablock ParticleData(AEBaseTaserFlashParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 50;
	lifetimeVarianceMS   = 25;
	textureName          = "base/data/particles/chunk";
	spinSpeed		= 10.0;
	spinRandomMin		= -1000.0;
	spinRandomMax		= 1000.0;
	colors[0]	= "1 1 0 0.5";
	colors[1]	= "1 1 0 0.3";
	sizes[0]      = 1;
	sizes[1]      = 0.1;

	useInvAlpha = false;
};

datablock ParticleEmitterData(AEBaseTaserFlashEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 20.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.1;
   thetaMin         = 0;
   thetaMax         = 15;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "AEBaseTaserFlashParticle";

   uiName = "";
};