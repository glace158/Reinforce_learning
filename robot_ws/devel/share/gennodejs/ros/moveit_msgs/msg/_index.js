
"use strict";

let MoveGroupActionGoal = require('./MoveGroupActionGoal.js');
let MoveGroupResult = require('./MoveGroupResult.js');
let PickupActionResult = require('./PickupActionResult.js');
let ExecuteTrajectoryGoal = require('./ExecuteTrajectoryGoal.js');
let PlaceAction = require('./PlaceAction.js');
let ExecuteTrajectoryActionGoal = require('./ExecuteTrajectoryActionGoal.js');
let PickupActionFeedback = require('./PickupActionFeedback.js');
let MoveGroupSequenceResult = require('./MoveGroupSequenceResult.js');
let PickupActionGoal = require('./PickupActionGoal.js');
let PickupAction = require('./PickupAction.js');
let PlaceGoal = require('./PlaceGoal.js');
let MoveGroupSequenceActionResult = require('./MoveGroupSequenceActionResult.js');
let ExecuteTrajectoryActionFeedback = require('./ExecuteTrajectoryActionFeedback.js');
let PickupFeedback = require('./PickupFeedback.js');
let MoveGroupSequenceAction = require('./MoveGroupSequenceAction.js');
let ExecuteTrajectoryAction = require('./ExecuteTrajectoryAction.js');
let MoveGroupActionFeedback = require('./MoveGroupActionFeedback.js');
let MoveGroupGoal = require('./MoveGroupGoal.js');
let ExecuteTrajectoryFeedback = require('./ExecuteTrajectoryFeedback.js');
let MoveGroupSequenceActionFeedback = require('./MoveGroupSequenceActionFeedback.js');
let PlaceActionGoal = require('./PlaceActionGoal.js');
let MoveGroupAction = require('./MoveGroupAction.js');
let ExecuteTrajectoryResult = require('./ExecuteTrajectoryResult.js');
let MoveGroupActionResult = require('./MoveGroupActionResult.js');
let MoveGroupSequenceFeedback = require('./MoveGroupSequenceFeedback.js');
let PlaceActionResult = require('./PlaceActionResult.js');
let MoveGroupSequenceGoal = require('./MoveGroupSequenceGoal.js');
let PlaceActionFeedback = require('./PlaceActionFeedback.js');
let ExecuteTrajectoryActionResult = require('./ExecuteTrajectoryActionResult.js');
let PickupResult = require('./PickupResult.js');
let PickupGoal = require('./PickupGoal.js');
let MoveGroupFeedback = require('./MoveGroupFeedback.js');
let MoveGroupSequenceActionGoal = require('./MoveGroupSequenceActionGoal.js');
let PlaceResult = require('./PlaceResult.js');
let PlaceFeedback = require('./PlaceFeedback.js');
let CostSource = require('./CostSource.js');
let CartesianPoint = require('./CartesianPoint.js');
let CartesianTrajectoryPoint = require('./CartesianTrajectoryPoint.js');
let PositionConstraint = require('./PositionConstraint.js');
let RobotState = require('./RobotState.js');
let ConstraintEvalResult = require('./ConstraintEvalResult.js');
let KinematicSolverInfo = require('./KinematicSolverInfo.js');
let GripperTranslation = require('./GripperTranslation.js');
let PlanningScene = require('./PlanningScene.js');
let MotionPlanRequest = require('./MotionPlanRequest.js');
let GenericTrajectory = require('./GenericTrajectory.js');
let PositionIKRequest = require('./PositionIKRequest.js');
let WorkspaceParameters = require('./WorkspaceParameters.js');
let MotionSequenceResponse = require('./MotionSequenceResponse.js');
let MotionSequenceRequest = require('./MotionSequenceRequest.js');
let CollisionObject = require('./CollisionObject.js');
let MotionPlanDetailedResponse = require('./MotionPlanDetailedResponse.js');
let DisplayTrajectory = require('./DisplayTrajectory.js');
let PlanningSceneComponents = require('./PlanningSceneComponents.js');
let RobotTrajectory = require('./RobotTrajectory.js');
let JointConstraint = require('./JointConstraint.js');
let CartesianTrajectory = require('./CartesianTrajectory.js');
let PlannerInterfaceDescription = require('./PlannerInterfaceDescription.js');
let PlaceLocation = require('./PlaceLocation.js');
let MotionPlanResponse = require('./MotionPlanResponse.js');
let LinkPadding = require('./LinkPadding.js');
let ContactInformation = require('./ContactInformation.js');
let AttachedCollisionObject = require('./AttachedCollisionObject.js');
let PlannerParams = require('./PlannerParams.js');
let AllowedCollisionEntry = require('./AllowedCollisionEntry.js');
let AllowedCollisionMatrix = require('./AllowedCollisionMatrix.js');
let JointLimits = require('./JointLimits.js');
let Grasp = require('./Grasp.js');
let TrajectoryConstraints = require('./TrajectoryConstraints.js');
let OrientationConstraint = require('./OrientationConstraint.js');
let VisibilityConstraint = require('./VisibilityConstraint.js');
let LinkScale = require('./LinkScale.js');
let MoveItErrorCodes = require('./MoveItErrorCodes.js');
let DisplayRobotState = require('./DisplayRobotState.js');
let Constraints = require('./Constraints.js');
let PlanningSceneWorld = require('./PlanningSceneWorld.js');
let BoundingVolume = require('./BoundingVolume.js');
let MotionSequenceItem = require('./MotionSequenceItem.js');
let ObjectColor = require('./ObjectColor.js');
let PlanningOptions = require('./PlanningOptions.js');
let OrientedBoundingBox = require('./OrientedBoundingBox.js');

module.exports = {
  MoveGroupActionGoal: MoveGroupActionGoal,
  MoveGroupResult: MoveGroupResult,
  PickupActionResult: PickupActionResult,
  ExecuteTrajectoryGoal: ExecuteTrajectoryGoal,
  PlaceAction: PlaceAction,
  ExecuteTrajectoryActionGoal: ExecuteTrajectoryActionGoal,
  PickupActionFeedback: PickupActionFeedback,
  MoveGroupSequenceResult: MoveGroupSequenceResult,
  PickupActionGoal: PickupActionGoal,
  PickupAction: PickupAction,
  PlaceGoal: PlaceGoal,
  MoveGroupSequenceActionResult: MoveGroupSequenceActionResult,
  ExecuteTrajectoryActionFeedback: ExecuteTrajectoryActionFeedback,
  PickupFeedback: PickupFeedback,
  MoveGroupSequenceAction: MoveGroupSequenceAction,
  ExecuteTrajectoryAction: ExecuteTrajectoryAction,
  MoveGroupActionFeedback: MoveGroupActionFeedback,
  MoveGroupGoal: MoveGroupGoal,
  ExecuteTrajectoryFeedback: ExecuteTrajectoryFeedback,
  MoveGroupSequenceActionFeedback: MoveGroupSequenceActionFeedback,
  PlaceActionGoal: PlaceActionGoal,
  MoveGroupAction: MoveGroupAction,
  ExecuteTrajectoryResult: ExecuteTrajectoryResult,
  MoveGroupActionResult: MoveGroupActionResult,
  MoveGroupSequenceFeedback: MoveGroupSequenceFeedback,
  PlaceActionResult: PlaceActionResult,
  MoveGroupSequenceGoal: MoveGroupSequenceGoal,
  PlaceActionFeedback: PlaceActionFeedback,
  ExecuteTrajectoryActionResult: ExecuteTrajectoryActionResult,
  PickupResult: PickupResult,
  PickupGoal: PickupGoal,
  MoveGroupFeedback: MoveGroupFeedback,
  MoveGroupSequenceActionGoal: MoveGroupSequenceActionGoal,
  PlaceResult: PlaceResult,
  PlaceFeedback: PlaceFeedback,
  CostSource: CostSource,
  CartesianPoint: CartesianPoint,
  CartesianTrajectoryPoint: CartesianTrajectoryPoint,
  PositionConstraint: PositionConstraint,
  RobotState: RobotState,
  ConstraintEvalResult: ConstraintEvalResult,
  KinematicSolverInfo: KinematicSolverInfo,
  GripperTranslation: GripperTranslation,
  PlanningScene: PlanningScene,
  MotionPlanRequest: MotionPlanRequest,
  GenericTrajectory: GenericTrajectory,
  PositionIKRequest: PositionIKRequest,
  WorkspaceParameters: WorkspaceParameters,
  MotionSequenceResponse: MotionSequenceResponse,
  MotionSequenceRequest: MotionSequenceRequest,
  CollisionObject: CollisionObject,
  MotionPlanDetailedResponse: MotionPlanDetailedResponse,
  DisplayTrajectory: DisplayTrajectory,
  PlanningSceneComponents: PlanningSceneComponents,
  RobotTrajectory: RobotTrajectory,
  JointConstraint: JointConstraint,
  CartesianTrajectory: CartesianTrajectory,
  PlannerInterfaceDescription: PlannerInterfaceDescription,
  PlaceLocation: PlaceLocation,
  MotionPlanResponse: MotionPlanResponse,
  LinkPadding: LinkPadding,
  ContactInformation: ContactInformation,
  AttachedCollisionObject: AttachedCollisionObject,
  PlannerParams: PlannerParams,
  AllowedCollisionEntry: AllowedCollisionEntry,
  AllowedCollisionMatrix: AllowedCollisionMatrix,
  JointLimits: JointLimits,
  Grasp: Grasp,
  TrajectoryConstraints: TrajectoryConstraints,
  OrientationConstraint: OrientationConstraint,
  VisibilityConstraint: VisibilityConstraint,
  LinkScale: LinkScale,
  MoveItErrorCodes: MoveItErrorCodes,
  DisplayRobotState: DisplayRobotState,
  Constraints: Constraints,
  PlanningSceneWorld: PlanningSceneWorld,
  BoundingVolume: BoundingVolume,
  MotionSequenceItem: MotionSequenceItem,
  ObjectColor: ObjectColor,
  PlanningOptions: PlanningOptions,
  OrientedBoundingBox: OrientedBoundingBox,
};
