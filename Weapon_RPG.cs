datablock AudioDescription(ExplosionDesc3d)
{
	Volume = 0.5;
	isLooping = false;
	isStreaming = false;
	is3D = true;
	maxDistance = 300;
	ReferenceDistance = 2000;
	coneInsideAngle = 360;
	coneOutsideAngle = 360;
	coneOutsideVolume = 1;
	coneVector = "0 0 1";
	enviornmentLevel = 0;
	loopCount = -1;
	minLoopGap = 0;
	maxLoopGap = 0;
	type = 2;
};

datablock AudioProfile(RPG7FireSound)
{
   filename    = "./Sounds/Fire/RPG/RPG_fire.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(RPG7IgniteSound)
{
   filename    = "./Sounds/Fire/RPG/RPGIGNITE.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(RPG7ExplodeSound)
{
   filename    = "./Sounds/Fire/RPG/RPGEXPLODE.wav";
   description = ExplosionClose3D;
   preload = true;
};

datablock AudioProfile(RPG7ExplodeDistSound)
{
   filename    = "./Sounds/Fire/RPG/RPGEXPLODDIST.wav";
   description = ExplosionFar3D;
   preload = true;
};

datablock AudioProfile(RPG7RocketLoopSound)
{
   filename    = "./Sounds/Fire/RPG/RPGLOOP.wav";
   description = BAADFireHeavyLoop3D;
   preload = true;
};

// RPG-7
datablock DebrisData(AERPG7MagDebris)
{
	shapeFile = "./RPG/rpgrocketinactive.dts";
	lifetime = 2.0;
	minSpinSpeed = 800.0;
	maxSpinSpeed = 800.0;
	elasticity = 0.5;
	friction = 0.1;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 3;
};
datablock ExplosionData(RPG7FailureExplosion : gunExplosion)
{
   emitter[0] = "";
   particleEmitter = "";
   lightStartRadius = 0;
   debris = AERPG7MagDebris;
   debrisNum = 1;
   debrisNumVariance = 0;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debristhetamin = 0;//45;
   debrisThetaMax = 45;
   debrisVelocity = 15;
   debrisVelocityVariance = 1;
};
datablock ProjectileData(RPG7FailedProjectile)
{
	directDamage        = 0;
   projectileShapeName = "base/data/shapes/empty.dts";
   armingDelay         = 300;
   lifetime            = 300;
   explodeOnDeath = true;
   
   muzzleVelocity      = 100;
   gravityMod = 0.0;
   
   destroyOnBounce = false;
   deleteOnBounce = false;
   
   hasLight    = false;
   explosion           = RPG7FailureExplosion;
   particleEmitter     = "";
   uiName = "";
};

datablock ExplosionData(RPG7KickoffExplosion)
{
   explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = RPG7IgniteSound;

   lifeTimeMS = 150;

   debris = tankShellDebris;
   debrisNum = 15;
   debrisNumVariance = 5;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debristhetamin = 135;//45;
   debrisThetaMax = 180;
   debrisVelocity = 70;//-70;
   debrisVelocityVariance = 50;

   particleEmitter = "";
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = RocketExplosionRingEmitter;
   emitter[1] = rocketLauncherFlashEmitter;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "3.0 10.0 3.0";
   camShakeDuration = 0.5;
   camShakeRadius = 32.0;

   // Dynamic light
   lightStartRadius = 5;
   lightEndRadius = 20;
   lightStartColor = "1 1 0 1";
   lightEndColor = "1 0 0 0";

   damageRadius = 3;
   radiusDamage = 100;

   impulseRadius = 6;
   impulseForce = 400;

   playerBurnTime = 0;
};

datablock ParticleData(RPG7BlastHazeParticle)
{
	dragCoefficient		= 2.5;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 850;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= -200.0;
	spinRandomMax		= 200.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "Add-Ons/Weapon_AEBase/Particles/genericflash2";

	//colors[0]	= "1 1 0.3 0.0";
	//colors[1]	= "0.9 0.6 0.0 1.0";
	//colors[2]	= "0.6 0.0 0.0 0.0";
	colors[0] = "1 0.6 0.5 0.0";
  colors[1] = "0.9 0.4 0.3 1";
  colors[2] = "0.9 0.4 0.3 0.0";

	sizes[0]	= 8.5;
	sizes[1]	= 7.35;
	sizes[2]	= 10.5;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(RPG7BlastHazeEmitter)
{
	ejectionPeriodMS = 8;
	periodVarianceMS = 0;
	ejectionVelocity = 13;
	velocityVariance = 3;
	ejectionOffset = 0.2;
	thetaMin         = 0.0;
	thetaMax         = 90.0;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;

  particles = "RPG7BlastHazeParticle";
};

datablock ParticleData(RPG7BlastSmokeParticle)
{
	dragCoefficient		= 2.5;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.3;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 3500;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= -200.0;
	spinRandomMax		= 200.0;
	useInvAlpha		= true;
	animateTexture		= false;

	textureName		= "base/data/particles/cloud";

	colors[0]	= "0.3 0.3 0.3 0.0";
	colors[1]	= "0.1 0.1 0.1 0.05";
	colors[2]	= "0.0 0.0 0.0 0.0";

	sizes[0]	= 0.5;
	sizes[1]	= 4.35;
	sizes[2]	= 3.1;

	times[0]	= 0.0;
	times[1]	= 0.3;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(RPG7BlastSmokeEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 10;
	velocityVariance = 5;
	ejectionOffset = 0.3;
	thetaMin         = 0.0;
	thetaMax         = 90.0;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;

  particles = "RPG7BlastSmokeParticle";
};

datablock ParticleData(RPG7BlastDebrisParticle)
{
	dragCoefficient = 0;
	gravityCoefficient = 5;
	inheritedVelFactor = 0.2;
	constantAcceleration = 0;
	lifetimeMS = 1000;
	lifetimeVarianceMS = 500;
	textureName = "base/data/particles/chunk";
	spinSpeed = 10;
	spinRandomMin = -500;
	spinRandomMax = 500;
	colors[0] = "0 0 0 1";
	colors[1] = "0 0 0 0";
	sizes[0] = 0.35;
	sizes[1] = 0.35;
	useInvAlpha = true;
};

datablock ParticleEmitterData(RPG7BlastDebrisEmitter)
{
	ejectionPeriodMS = 3;
	periodVarianceMS = 0;
	ejectionVelocity = 25;
	velocityVariance = 6;
	ejectionOffset = 0;
	thetaMin = 0;
	thetaMax = 90;//30;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;

  particles = "RPG7BlastDebrisParticle";
};

datablock ParticleData(RPG7BlastDebrisParticle)
{
     dragCoefficient		= 5.0;
     windCoefficient		= 1.0;
     gravityCoefficient	= 0;
     inheritedVelFactor	= 0.0;
     constantAcceleration	= 0.0;
     lifetimeMS		= 950;
     lifetimeVarianceMS	= 250;
     spinSpeed		= 5.0;
     spinRandomMin		= -5.0;
     spinRandomMax		= 5.0;
     useInvAlpha		= false;
     animateTexture		= false;
     //framesPerSec		= 1;

     textureName		= "Add-Ons/Weapon_AEBase/Particles/genericflash2";
     //animTexName		= "~/data/particles/cloud";

     // Interpolation variables
	colors[0] = "1 0.6 0.5 0.0";
  colors[1] = "0.9 0.4 0.3 0.8";
  colors[2] = "0.9 0.4 0.3 0.0";
  colors[3] = "0.9 0.4 0.3 0.0";

	sizes[0]	= 0.2;
	sizes[1]	= 0.4;
	sizes[2]	= 1.9;
	sizes[3]	= 2.8;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(RPG7BlastDebrisEmitter)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 0;
   velocityVariance = 2.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 1;
   phiReferenceVel  = 30;
   phiVariance      = 32;
   overrideAdvance = false;
   particles = "RPG7BlastDebrisParticle";
};

datablock DebrisData(RPG7BlastDebrisData)
{
	emitters = RPG7BlastDebrisEmitter;

	shapeFile = "base/data/shapes/empty.dts";
	lifetime = 0.3;
	minSpinSpeed = -1000.0;
	maxSpinSpeed = 1000.0;
	elasticity = 0;
	friction = 0;
	numBounces = 0;
	staticOnMaxBounce = false;
	snapOnMaxBounce = false;
	fade = false;

	gravModifier = 0.5;
};

datablock ExplosionData(RPG7Explosion)
{
   //explosionShape = "";
	explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = RPG7ExplodeSound;

	lifeTimeMS = 350;

	particleEmitter = RPG7BlastDebrisEmitter;
	particleDensity = 60;
	particleRadius = 0.35;

	emitter[0] = RPG7BlastHazeEmitter;
	emitter[1] = RPG7BlastSmokeEmitter;

	debris = RPG7BlastDebrisData;
	debrisNum = 30;
	debrisNumVariance = 10;
	debrisPhiMin = 0;
	debrisPhiMax = 360;
	debrisThetaMin = 0;
	debrisThetaMax = 180;
	debrisVelocity = 32;
	debrisVelocityVariance = 5;

	faceViewer     = true;
	explosionScale = "2 2 2";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	// Dynamic light
	lightStartRadius = 10;
	lightEndRadius = 25;
	lightStartColor = "1 1 1 1";
	lightEndColor = "0 0 0 1";

	damageRadius = 12;
	radiusDamage = 300;

	impulseRadius = 24;
	impulseForce = 1000;
};

datablock ProjectileData(RPG7Projectile)
{
   projectileShapeName = "./RPG/RPGRocket.dts";
   directDamage        = 100;
   directDamageType = $DamageType::AE;
   radiusDamageType = $DamageType::AE;
   impactImpulse	   = 1;
   verticalImpulse	   = 1000;
   explosion           = RPG7Explosion;
   particleEmitter     = rocketTrailEmitter;

   brickExplosionRadius = 3;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 30;             
   brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

   sound = RPG7RocketLoopSound;

   muzzleVelocity      = 200;
   velInheritFactor    = 0;

   armingDelay         = 00;
   lifetime            = 16000;
   fadeDelay           = 15500;
   bounceElasticity    = 0.5;
   bounceFriction       = 0.20;
   isBallistic         = true;
   gravityMod = 0.25;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "1 0.5 0.0";

   uiName = "RPG-7 Rocket";
};

function RPG7Projectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function RPG7Projectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	%src = %obj.getTransform();
	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%cc = ClientGroup.getObject(%i);
		%targ = %cc.getControlObject();
		if(!isObject(%targ))
			continue;

		if(vectorDist(%targ.getTransform(), %src) > 50)
			%cc.play3D(RPG7ExplodeDistSound, %src);
	}
	AETrailedProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function RPG7Projectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

datablock ProjectileData(RPG7ProjectileInactiveProjectile : RPG7Projectile)
{
   directDamage        = 100;
   directDamageType = $DamageType::AE;
	
   projectileShapeName = "./RPG/RPGRocketInactive.dts";
   armingDelay         = 200;
   lifetime            = 200;
   explodeOnDeath = true;
   
   muzzleVelocity      = 100;
   gravityMod = 0.0;
   
   destroyOnBounce = true;
   deleteOnBounce = true;
   bounceDestroyProjectile = RPG7FailedProjectile;
   activatedProjectile = RPG7Projectile;
   
   
   hasLight    = false;
   explosion           = RPG7KickoffExplosion;
   particleEmitter     = "";
   uiName = "";
};

function RPG7ProjectileInactiveProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function RPG7ProjectileInactiveProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

//END OF STUFF

datablock ParticleData(RPG7LauncherSmokeParticle)
{
	dragCoefficient      = 6;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 15;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 500.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "1.0 1.0 0.3 0.6";
	colors[1]     = "1.0 0.7 1.0 0.0";
	sizes[0]      = 0;
	sizes[1]      = 2;


	useInvAlpha = false;
};
datablock ParticleEmitterData(RPG7LauncherSmokeEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = -50.0;
   velocityVariance = 10.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 1;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "RPG7LauncherSmokeParticle";

   uiName = "";
};

//////////
// item //
//////////
datablock ItemData(RPG7Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./RPG/RPG.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: RPG-7";
	iconName = "./Icons/44";
	doColorShift = true;
	colorShiftColor = "0.6 0.6 0.6 1";

	 // Dynamic properties defined by the scripts
	image = RPG7Image;
	canDrop = true;

	AEAmmo = 1;
	AEType = AE_RocketLAmmoItem.getID();
	AEBase = 1;

	RPM = 30;
	recoil = "Holy";
	uiColor = "1 1 1";
	description = "Fully non lethal RPG7. Ok, it might still be a little lethal... ";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(RPG7Image)
{
   // Basic Item properties
   shapeFile = "./RPG/RPG.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0.075 0.075";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = RPG7Item;
   ammo = " ";
   projectile = RPG7Projectile;
   projectileType = Projectile;

   Mag = a;
   shellExitDir        = "-1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = RPG7SafetyImage;
    scopingImage = RPG7IronsightImage;
	doColorShift = true;
	colorShiftColor = RPG7Item.colorShiftColor;//"0.400 0.196 0 1.000";

	muzzleFlashScale = "2 2 2";
	bulletScale = "1 1 1";

	screenshakeMin = "0.5 0.5 0.5"; 
	screenshakeMax = "1 1 1"; 

	projectileDamage = 100;
	projectileCount = 1;
	projectileHeadshotMult = 2;
	projectileVelocity = 100;
	projectileTagStrength = 1;  // tagging strength
	projectileTagRecovery = 0.02; // tagging decay rate
    alwaysSpawnProjectile = true;
	projectileVehicleDamageMult = 0.1;
	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadReset = 0; // m
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;

   //Mag = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.
   // Initial start up state
	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.01;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "Reload";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateEmitter[2]					= RPG7LauncherSmokeEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= tailNode;
	stateScript[2]                     = "AEOnFire";
	stateFire[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateTimeoutValue[3]             	= 0.65;
	stateEmitter[3]					= rocketLauncherSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.1;
	stateTransitionOnTimeout[5]		= "LoadCheckB";
	
	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6] = "Empty";
	stateTransitionOnNoAmmo[6]		= "Reload";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.4;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagIn";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.75;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";
	stateSound[9]				= RPG7MagInSound;
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.4;
	stateTransitionOnTimeout[10]		= "Cocka";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";
	
	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.065;
	stateTransitionOnTimeout[11]		= "FireLoadCheckB";
	
	stateName[12]				= "FireLoadCheckB";
	stateTransitionOnNoAmmo[12]		= "Reload";
	stateTransitionOnAmmo[12]  = "Ready";
	stateTransitionOnNotLoaded[12]  = "Ready";
		
	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.1;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "Ready";
	
	stateName[20]				= "ReadyLoop";
	stateTransitionOnTimeout[20]		= "Ready";

	stateName[21]          = "Empty";
	stateTransitionOnTriggerDown[21]  = "Dryfire";
	stateTransitionOnLoaded[21] = "Reload";
	stateScript[21]        = "AEOnEmpty";

	stateName[22]           = "Dryfire";
	stateTransitionOnTriggerUp[22] = "Empty";
	stateWaitForTimeout[22]    = false;
	stateScript[22]      = "onDryFire";
	
	stateName[23]           = "SemiAutoCheck"; //heeeeeeeeeeeeey
	stateTransitionOnTriggerUp[23]	  	= "FireLoadCheckA";
	
	stateName[24]           = "Cocka";
	stateScript[24]				= "onCock";
	stateSequence[24]			= "Cock";
	stateTimeoutValue[24]			= 0.35;
	stateTransitionOnTimeout[24]		= "Ready";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function RPG7Image::AEOnFire(%this,%obj,%slot)
{	
    %obj.schedule(50, "aeplayThread", "2", "jump");
	%obj.stopAudio(0); 
  %obj.playAudio(0, RPG7FireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(750, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function RPG7Image::onCock(%this, %obj, %slot)
{
    %obj.schedule(100, playAudio, 1, RPG7CockSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function RPG7Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function RPG7Image::onReloadMagIn(%this,%obj,%slot)
{
    %obj.schedule(300, "aeplayThread", "3", "plant");
    %obj.schedule(300, "aeplayThread", "2", "shiftright");
    %obj.schedule(350, playAudio, 1, RPG7TwistSound);
}

function RPG7Image::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function RPG7Image::onReload2End(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function RPG7Image::onReloadStart(%this,%obj,%slot)
{
   %obj.reload3Schedule = %this.schedule(250,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(700,800),0,serverPlay3D,AEMagPlasticAr @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function RPG7Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function RPG7Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function RPG7Image::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(RPG7SafetyImage)
{
   shapeFile = "./RPG/RPG.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0.075 0.075";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = RPG7Item;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   safetyImage = RPG7Image;
   doColorShift = true;
   colorShiftColor = RPG7Item.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateWaitForTimeout[0]		  	= false;
	stateTransitionOnTimeout[0]     	= "";
	stateSound[0]				= "";

};

function RPG7SafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function RPG7SafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(RPG7IronsightImage : RPG7Image)
{
	recoilHeight = 0.25;

	scopingImage = RPG7Image;
	sourceImage = RPG7Image;
	
  offset = "0 0.075 0.075";
	eyeOffset = "0.193 1 -0.4569";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.5;
   
	stateName[7]				= "Reload";
	stateScript[7]				= "onDone";
	stateTimeoutValue[7]			= 1;
	stateTransitionOnTimeout[7]		= "";
	stateSound[7]				= "";
};

function RPG7IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function RPG7IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function RPG7IronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	%obj.stopAudio(0); 
  %obj.playAudio(0, RPG7FireSound);

	Parent::AEOnFire(%this, %obj, %slot);
}

function RPG7IronsightImage::onCock(%this, %obj, %slot)
{
    %obj.schedule(250, playAudio, 1, RPG7CockSound);
}

function RPG7IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function RPG7IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn5Sound);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function RPG7IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}