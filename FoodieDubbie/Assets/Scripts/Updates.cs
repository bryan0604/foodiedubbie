#region To Updates
// 1. Level 2 Features
// 2. Balancing Level 1 - Intermediate
// 3. Multiplayer
// 4. Level 3 Features
// 5. Create skill name for Upgraded Abilities!
// 6. Boss Dialogue text system.
// 7.
// 8. AOE damage indications basic - animations code mechanics. (Neglected)
// 9. Special Effects when player takes dmg.
//10. Display Damage text when got hit.
//11. Main Menu introduction - Design.
//12. Main Menu - functions basic.
//13. In Game - Pause Function.
//14. 
//15. AOE Introduction - Large.
//16. Time potion - Add / Reduce time. 
//17. Time = 0. End game. 
//18. Reconstruct a better system of Boss Manager and Game Manager. (Check theory below)
#endregion

#region Updates Done
// 1. Players Movements
// 2. Players Mobile Phone with Joystick Movements
// 3. Players User Interfaces.
// 4. Players Health Points Implementation.
// 5. Platform Design Level 1 - Basic.
// 6. Main Screen - User Interface.
// 7. AOE damage indications basic - animations. 
// 8. Game Manager handling for Boss Fight Mechanics - Basic.
// 9. AOE damage Indications Basic.
// 10. AOE introduction - Single Target at Player position.
// 11. AOE introduction - Single Random at Playzone.
// 12. AOE introductiin - Multiple Target at Player position.
// 13. AOE introduction - Multiple Random at Playzone.
// 14. Special Effects - On Explosion
// 15. Food Introduction - Good and Bad. Control by Spawn Management
// 16. Boss Management - Health Points
// 17. Health Bar background lerp to correct point.
// 18. AOE area hit range.
// 19. Spawn Management on Platform
// 20. Boss Management - Pattern.
// 21. Buffs Management - Advantage & Disadvantage buffs
// 22. Buffs Disadvantage - Follow player in range
// 23. Ultilised boss fight pattern.
// 24. Special Effects for Turret
// 25. Special Effects for Normal attack
// 26. Special Effects for Aoe explode.
// 27. Differentiate boss fight pattern with Phases.
// 28. Ultilising Phase Management.
// 29. Boss UI health bar introduction
// 30. Boss UI health bar background lerping.
// 31. Player UI healthbar background lerpings.
// 32 Player UI lerping to player position.
// 33. Boss fight pattern updated with simple accessible through other scripts
// 34. Balancing Level 1 - Standard
#endregion

#region Errors
// 1. Game Manager singleton not found is null. On Start issue.
// 2. Buff Disadvantage needed to clear target set null when disabled.
#endregion

#region Errors Fixed
//1. Android build technical errors
//2. Object not found in game on Start - Joystick
//3. Object not found in game on start - Player
//4. Object not found in game on start - rigidbody
//5. Removed OnValidate functions from script.
//6. Boss not changing phase.
//7. Boss phase does not tally with current phase.
//8. Game Object failed pooling.
//9. Game object pooled wrongly.
//10. OnCollision error misunderstood with OnTriggers.
//11. Spawn outside platform.
//12. Misloop certain boss skills
//13. UIs issues after set to mobile platform.
//14. Unity crashed due to Ienumerator fault OnValidate
//15. Unity GUI Editor Windows issues doesnt update infos.

#endregion

#region Comments and Pointers
//1. Make sure gameplay will make player busy running around.
//2. If too easy. decrease aoe explode time.
//3. Challenge Defeat boss and Time Taken.
//4. Adjust difficulties to maximum if too boring
#endregion

#region Boss Lv 1 Fight pattern.
#endregion

#region Code Logic
// All bosses can access the same Boss Management code.
// Boss Management code can check boss levels.
// According to their level. Specific design boss fight pattern will be activate.
// Boss Management code allows them to cast abilities pattern such as SingleTarget, Single Random, MultiTarget, MultiRandom. (For Now)
// When an abilities is cast. Game Manager will pull AOE object to needed position from AOE pool. 
// When AOE object exploded. It will trigger Game Manager to pull back to AOE pool.

//AOE pool logic (Game Manager)
// Instantiate the amount required On Start
// Add them(aoes) to a List.
// AOEs contain lv1 lv2 lv3 and so on...
// AOE manager itself will define their specifics level.

// Boss Abilities (Boss Management)
// When boss lv1 cast i.e SingleTarget ability.
// Game Manager will receive a trigger and pull AOEs from pool.
// Boss Manager will pass in boss info such as level 1 to Game Manager. 
// So, game manager will be releasing a single AOE lv 1 to player position. 
// Game Manager will loop through AOE pool for lv 1 AOE.
// If found = true, Game Manager setactive for that AOE object and set it position to Player position.

// Game Manager randomly pick a point on Platform.
// public bool foundAPlatform
// public Vector3 targetPoint
// void PickAPoint()
// {
// blah blah blah...
// if a point is touch on platform. foundAPlatform = true
// targetpoint = that point.
// }
// then use targetpoint 
#endregion

#region Boss Fight Summary
// Big Boss fight Comes At every 3 stages
// Boss lv 1 - small aoe, buffs 
// Boss lv 2 - small aoe, buffs, more aoes, fast explode, fast buffs, more health
// Boss lv 3 - Platforms moving and does dmg, A pattern Aoe, Big Aoe at midpt only(last phase), small aoes, Higher aoe dmg, dbuffs affect player, buffs, Fast explode. 
// New Platform Designs
// Boss lv 4 - Small aoes, buffs, small line Aoe. 
// Boss lv 5 - Small aoes, Big Aoe, buffs, more small line Aoes 
// Boss Lv 6 - Moving platform(it will guide hint player must stay in that area and next safe area), small aoes, buffs, dbuffs affect player, Large line aoe
// Boss Lv 7 - dbuffs affect player, small line aoes, small aoes.
// Boss Lv 8 - dbuffs affect player, small line aoes, small aoes, large aoe(at player pos)
// Boss Lv 9 - Solve Puzzle Tactic(a buff that need to place at correct area, Large Line aoe
// Boss lv 10 - Large aoes(not too much), buffs, dbuffs affect player, Small line aoes, small aoes, towers that heals boss (need to be destroy by turret depending on conditions refer below.)


// Boss lv 12 - Large line aoe. Changing Platform to fight boss...

// Boss lv 15 - Drop massive little balls with gravity.

// Features 
// - Obstzcles blocking players
//- dbuffs exploded to flames on ground. 
// - flames stays for durations on ground.
// - stay in specific area for health regen.

//Conditionz
// If dbuffs hit tower and destroyed 
// - must clear within next tower in 60 secs
// - else heals
#endregion