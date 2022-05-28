datablock AudioProfile(BNE_TaserFireSound)
{
   filename    = "./Sounds/Fire/Taser/Taser_fire.wav";
   description = LightClose3D;
   preload = true;
};

datablock AudioProfile(BNE_TasedSound)
{
   filename    = "./Sounds/Fire/Taser/Tased.wav";
   description = LightClose3D;
   preload = true;
};

// Taser
datablock DebrisData(BNE_TaserMagDebris)
{
	shapeFile = "./Taser/TaserMag.dts";
	lifetime = 2.0;
	minSpinSpeed = -700.0;
	maxSpinSpeed = -600.0;
	elasticity = 0.5;
	friction = 0.1;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};

//STUFF

datablock ParticleData(BNE_taserTrailParticle)
{
	  gravityCoefficient   = -1;
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 300;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= false;
	animateTexture		= false;
	textureName		= "base/data/particles/ring";

	//Interpolation variables
	colors[0]	= "1 1 0 0.5";
	colors[1]	= "1 1 0 0.6";
	colors[2]	= "1 1 0 0.3";
	sizes[0]	= 0.25;
	sizes[1]	= 0.25;
	sizes[2]	= 0.0;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(BNE_taserTrailEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 1;
   ejectionVelocity = 1;
   ejectionOffset   = 0.4;
   velocityVariance = 1;
   thetaMin         = 0;
   thetaMax         = 30;
   phiReferenceVel  = 42000;
   phiVariance      = 0;
   overrideAdvance = false;
   particles = "BNE_taserTrailParticle";
   uiName = "taserTrail";

};

datablock ParticleData(BNE_taserExplosionParticle)
{
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0;
	constantAcceleration	= 10.0;
	lifetimeMS		= 600;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "base/data/particles/chunk";
	//Interpolation variables
	colors[0]	= "1 1 0 0.5";
	colors[1]	= "1 1 0 0.6";
	colors[2]	= "1 1 0 0.3";

	sizes[0]	= 1;
	sizes[1]	= 1;
	sizes[2]	= 0.00;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(BNE_taserExplosionEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 7;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "BNE_taserExplosionParticle";

   useEmitterColors = true;
   uiName = "Taser Explosion";
};

datablock ExplosionData(BNE_taserExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   emitter[0] = BNE_taserExplosionEmitter;
   particleDensity = 1000;
   particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.0;
   camShakeRadius = 0.0;

   // Dynamic light
   lightStartRadius = 5;
   lightEndRadius = 1;
   lightStartColor = "1 1 0.0";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 3.5;
   impulseForce = 500;

   //radius damage
   radiusDamage        = 1;
   damageRadius        = 0.5;
};


datablock ProjectileData(BNE_taserProjectile : AETrailedProjectile)
{
   explosion           = BNE_taserExplosion;
   particleEmitter     = BNE_taserTrailEmitter;

    lifetime            = 1000;
    fadeDelay           = 750;

   brickExplosionRadius = 0;
   brickExplosionImpact = false; //destroy a brick if we hit it directly?
   brickExplosionForce  = 0;
   brickExplosionMaxVolume = 0;
   brickExplosionMaxVolumeFloating = 0;

   gravityMod = 1;
};

function BNE_taserProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function BNE_taserProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	if((%col.getType() & $typeMasks::playerObjectType) && isObject(%col.client))
	{
		%col.client.DropInventory();
		%col.clearTools();
		%col.stopAudio(3); 
		%col.playAudio(3, BNE_TasedSound);
	}
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

//END OF STUFF


//////////
// item //
//////////
datablock ItemData(BNE_TaserItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Taser/Taser.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Taser";
	iconName = "./Icons/44";
	doColorShift = true;
	colorShiftColor = "0.55 0.5 0.5 1";

	 // Dynamic properties defined by the scripts
	image = BNE_TaserImage;
	canDrop = true;

	AEAmmo = 2;
	AEType = AE_TaserAmmoItem.getID();
	AEBase = 1;

	RPM = 30;
	recoil = "No"; 
	uiColor = "1 1 1";
	description = "The XME brand made in America taser, loaded with mostly non lethal electric bolts." NL "This big fella is capable of stopping any fentanyl addicts dead in their tracks, guaranteed!";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_TaserImage)
{
   // Basic Item properties
   shapeFile = "./Taser/Taser.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 -0.05 0.05";
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
   item = BNE_TaserItem;
   ammo = " ";
   projectile = BNE_taserProjectile;
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
	safetyImage = BNE_TaserSafetyImage;
    scopingImage = BNE_TaserIronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_TaserItem.colorShiftColor;//"0.400 0.196 0 1.000";

	muzzleFlashScale = "0.25 0.25 0.25";
	bulletScale = "1 1 1";

	projectileDamage = 5;
	projectileCount = 1;
	projectileHeadshotMult = 2;
	projectileVelocity = 50;
	projectileTagStrength = 1;  // tagging strength
	projectileTagRecovery = 0.01; // tagging decay rate
	projectileTagIgnore = true; // ignore global tag multipliers
    alwaysSpawnProjectile = true;
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
	stateEmitter[2]					= AEBaseTaserFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateScript[2]                     = "AEOnFire";
	stateFire[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateTimeoutValue[3]             	= 0.65;
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
	stateTimeoutValue[7]			= 0.25;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";
	
	stateName[8]				= "ReloadMagOut";
	stateScript[8]				= "onReloadMagOut";
	stateTimeoutValue[8]			= 0.5;
	stateTransitionOnTimeout[8]		= "ReloadMagIn";
	stateSequence[8]			= "MagOut";
	stateSound[8]				= BNE_TaserMagOutSound;
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.35;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";
	stateSound[9]				= BNE_TaserMagInSound;
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.25;
	stateScript[10]				= "onReloadEnd";
	stateTransitionOnTimeout[10]		= "Ready";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";
	stateSound[10]				= BNE_TaserBoltSound;
	
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

function BNE_TaserImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.aeplayThread(2, shiftleft);
	%obj.aeplayThread(3, shiftright);
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_TaserFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(250, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_TaserImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_TaserImage::onReloadMagOut(%this,%obj,%slot)
{
  %obj.aeplayThread(2, shiftleft);
  %obj.aeplayThread(3, plant);
}

function BNE_TaserImage::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
   %obj.schedule(50, "aeplayThread", "3", "shiftright");
    %obj.schedule(300, "aeplayThread", "2", "shiftleft");
   %obj.schedule(400, "aeplayThread", "3", "plant");
}

function BNE_TaserImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_TaserImage::onReload2End(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function BNE_TaserImage::onReloadStart(%this,%obj,%slot)
{
   %obj.reload3Schedule = %this.schedule(250,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(700,800),0,serverPlay3D,AEMagPlasticAr @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_TaserImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_TaserImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_TaserImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_TaserImage::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_TaserMagImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_TaserMagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.15 0.5 0.35";
   rotation = eulerToMatrix( "0 0 0" );	
	
	casing = BNE_TaserMagDebris;
	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;	
	shellVelocity       = 4.0;
	
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

function BNE_TaserMagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_TaserSafetyImage)
{
   shapeFile = "./Taser/Taser.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_TaserItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_TaserIronsightImage;
   safetyImage = BNE_TaserImage;
   doColorShift = true;
   colorShiftColor = BNE_TaserItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_TaserSafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function BNE_TaserSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_TaserSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_TaserIronsightImage : BNE_TaserImage)
{
	recoilHeight = 0.25;

	scopingImage = BNE_TaserImage;
	sourceImage = BNE_TaserImage;
	
	isScopedImage = true;

	offset = "0 -0.05 0.05";
	eyeOffset = "0 1.25 -0.125";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 2;
	R_MovePenalty = 0.5;
   
	stateName[7]				= "Reload";
	stateScript[7]				= "onDone";
	stateTimeoutValue[7]			= 1;
	stateTransitionOnTimeout[7]		= "";
	stateSound[7]				= "";
};

function BNE_TaserIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_TaserIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_TaserIronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.blockImageDismount = true;
	%obj.schedule(250, unBlockImageDismount);

	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_TaserFireSound);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_TaserIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_TaserIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_TaserIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}