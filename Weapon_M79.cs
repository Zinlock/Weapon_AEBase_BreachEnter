datablock DebrisData(BNE_GrenadeShellDebris : AE_BEFiftyShellDebris)
{
	shapeFile = "./M79/m79shell.dts";
};

datablock AudioProfile(BNE_M79FireSound)
{
   filename    = "./Sounds/Fire/M79/M79_fire.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_M79ExplodeSound)
{
   filename    = "./Sounds/Fire/M79/M79EXPLODE.wav";
   description = ExplosionClose3D;
   preload = true;
};

datablock AudioProfile(BNE_M79ExplodeDistSound)
{
   filename    = "./Sounds/Fire/M79/M79EXPLODDIST.wav";
   description = ExplosionFar3D;
   preload = true;
};

// M79
datablock DebrisData(BNE_M79MagDebris)
{
	shapeFile = "./M79/M79Grenade.dts";
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

datablock ExplosionData(BNE_M79FailureExplosion : gunExplosion)
{
   emitter[0] = "";
   particleEmitter = "";
   lightStartRadius = 0;
   debris = BNE_M79MagDebris;
   debrisNum = 1;
   debrisNumVariance = 0;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debristhetamin = 0;//45;
   debrisThetaMax = 45;
   debrisVelocity = 15;
   debrisVelocityVariance = 1;
};

datablock ProjectileData(BNE_M79FailedProjectile)
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
   explosion           = BNE_M79FailureExplosion;
   particleEmitter     = "";
   uiName = "";
};

datablock ExplosionData(BNE_M79Explosion)
{
   //explosionShape = "";
	explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = BNE_M79ExplodeSound;

	lifeTimeMS = 350;

	particleEmitter = BNE_RPG7BlastDebrisEmitter;
	particleDensity = 60;
	particleRadius = 0.35;

	emitter[0] = BNE_RPG7BlastHazeEmitter;
	emitter[1] = BNE_RPG7BlastSmokeEmitter;

	debris = BNE_RPG7BlastDebrisData;
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

	damageRadius = 9;
	radiusDamage = 180;

	impulseRadius = 24;
	impulseForce = 1000;
};

datablock ProjectileData(BNE_M79Projectile)
{
   projectileShapeName = "./M79/M79Grenade.dts";
   directDamage        = 50;
   directDamageType = $DamageType::AE;
   radiusDamageType = $DamageType::AE;
   impactImpulse	   = 1;
   verticalImpulse	   = 1000;
   explosion           = BNE_M79Explosion;
   particleEmitter     = AERifleTrailEmitter;

   brickExplosionRadius = 3;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 30;             
   brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

   muzzleVelocity      = 100;
   velInheritFactor    = 0;

   armingDelay         = 00;
   lifetime            = 16000;
   fadeDelay           = 15500;
   bounceElasticity    = 0.5;
   bounceFriction       = 0.20;
   isBallistic         = true;
   gravityMod = 0.8;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "1 0.5 0.0";

   uiName = "M79 Grenade";
};

function BNE_M79Projectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function BNE_M79Projectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	%src = %obj.getTransform();
	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%cc = ClientGroup.getObject(%i);
		%targ = %cc.getControlObject();
		if(!isObject(%targ))
			continue;

		if(vectorDist(%targ.getTransform(), %src) > 50)
			%cc.play3D(BNE_M79ExplodeDistSound, %src);
	}
	AETrailedProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function BNE_M79Projectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

datablock ProjectileData(BNE_M79InactiveProjectile : BNE_M79Projectile)
{
   directDamage        = 50;
   directDamageType = $DamageType::AE;
	
   projectileShapeName = "./M79/M79Grenade.dts";
   armingDelay         = 200;
   lifetime            = 200;
   explodeOnDeath = true;
		particleEmitter     = AESilencedTrailEmitter;

   muzzleVelocity      = 100;
   gravityMod = 0.8;
   
   destroyOnBounce = true;
   deleteOnBounce = true;
   bounceDestroyProjectile = BNE_M79FailedProjectile;
   activatedProjectile = BNE_M79Projectile;
   
   hasLight    = false;
   explosion           = "";
   particleEmitter     = "";
   uiName = "";
};

function BNE_M79InactiveProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function BNE_M79InactiveProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

//END OF STUFF

datablock ParticleData(BNE_M79LauncherSmokeParticle)
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
datablock ParticleEmitterData(BNE_M79LauncherSmokeEmitter)
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
   particles = "BNE_M79LauncherSmokeParticle";

   uiName = "";
};

//////////
// item //
//////////
datablock ItemData(BNE_M79Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./M79/M79.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: M79";
	iconName = "./Icons/Icon_M79";
	doColorShift = true;
	colorShiftColor = "0.6 0.6 0.6 1";

	 // Dynamic properties defined by the scripts
	image = BNE_M79Image;
	canDrop = true;

	AEAmmo = 1;
	AEType = AE_GrenadeLAmmoItem.getID();
	AEBase = 1;

	RPM = 4;
	recoil = "Low";
	uiColor = "1 1 1";
	description = "The M79 is a big, strong and powerful Anti-Armor weapon produced in the USSR from 1961 to even today." NL "It recently saw adoption by Terrorist forces in the middle east. It is the perfect weapon for protecting your 72 virgins." NL "Also, stand behind somebody who is firing this thing for a nice surprise!";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_M79Image)
{
   // Basic Item properties
   shapeFile = "./M79/M79.dts";
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
   item = BNE_M79Item;
   ammo = " ";
   projectile = BNE_M79InactiveProjectile;
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
	safetyImage = BNE_M79SafetyImage;
    scopingImage = BNE_M79IronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_M79Item.colorShiftColor;//"0.400 0.196 0 1.000";

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	screenshakeMin = "0.3 0.3 0.3"; 
	screenshakeMax = "1 1 1";

	projectileDamage = 50;
	projectileCount = 1;
	projectileHeadshotMult = 1.5;
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
	stateTimeoutValue[7]			= 0.5;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";

	stateName[24]				= "ReloadMagOut";
	stateTimeoutValue[24]			= 0.6;
	stateScript[24]				= "onReloadMagOut";
	stateTransitionOnTimeout[24]		= "ReloadMagIn";
	stateWaitForTimeout[24]			= true;
	stateSequence[24]			= "ReloadOut";
	stateSound[24]				= BNE_M79ExtractSound;
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.4;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "ReloadIn";
	stateSound[9]				= BNE_M79InsertSound;
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.4;
	stateTransitionOnTimeout[10]		= "Reloaded";
	stateWaitForTimeout[10]			= true;
	stateScript[10]			  = "onReloadEnd";
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
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_M79Image::AEOnFire(%this,%obj,%slot)
{	
    %obj.schedule(50, "aeplayThread", "2", "jump");
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_M79FireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(750, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_M79Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_M79Image::onReloadMagOut(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftRight");
}

function BNE_M79Image::onReloadMagIn(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "plant");
}

function BNE_M79Image::onReloadEnd(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftAway");
	%obj.playAudio(1, BNE_M79CloseSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function BNE_M79Image::onReloadStart(%this,%obj,%slot)
{
  %obj.schedule(150, "aeplayThread", "2", "shiftLeft");
  %obj.schedule(150, "aeplayThread", "3", "plant");
   %obj.reload2Schedule = %obj.schedule(150,playAudio, 1, BNE_M79OpenSound);
   %obj.reload3Schedule = %this.schedule(500,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(950,1100),0,serverPlay3D,AEShellHeavyShotgun @ getRandom(1,2) @ Sound,%obj.getPosition());
}

function BNE_M79Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_M79Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_M79Image::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload2Schedule);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_M79Image::onMagDrop(%this,%obj,%slot)
{
	%a = new Camera()
	{
		datablock = Observer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};

	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_M79CasingImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_M79CasingImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.1 0.8	0.5";
   rotation = eulerToMatrix( "0 0 0" );

	casing = BNE_GrenadeShellDebris;
	shellExitDir        = "0.3 0 1";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;
	shellVelocity       = 5.5;

	stateName[0]					= "Ready";
	stateTimeoutValue[0]			= 0.01;
	stateTransitionOnTimeout[0] 	= "EjectA";

	stateName[1]					= "EjectA";
	stateEjectShell[1]				= true;
	stateTimeoutValue[1]			= 1;
	stateTransitionOnTimeout[1] 	= "Done";

	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};

function BNE_M79CasingImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_M79SafetyImage)
{
   shapeFile = "./M79/M79.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0.075 0.075";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_M79Item;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_M79IronsightImage;
   safetyImage = BNE_M79Image;
   doColorShift = true;
   colorShiftColor = BNE_M79Item.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_M79SafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function BNE_M79SafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_M79SafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_M79IronsightImage : BNE_M79Image)
{
	recoilHeight = 0.25;

	scopingImage = BNE_M79Image;
	sourceImage = BNE_M79Image;
	
  offset = "0 0.075 0.075";
	eyeOffset = "0.0035 0.8 -0.6";
	rotation = eulerToMatrix( "12 -20 0" );
	eyeRotation = eulerToMatrix("12 0 0");

	desiredFOV = $ae_HighIronsFOV;
	projectileZOffset = 6;
	R_MovePenalty = 0.5;
   
	stateName[7]				= "Reload";
	stateScript[7]				= "onDone";
	stateTimeoutValue[7]			= 1;
	stateTransitionOnTimeout[7]		= "";
	stateSound[7]				= "";
};

function BNE_M79IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_M79IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_M79IronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_M79FireSound);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_M79IronsightImage::onCock(%this, %obj, %slot)
{
    %obj.schedule(250, playAudio, 1, BNE_M79CockSound);
}

function BNE_M79IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_M79IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_M79IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}