
"use strict";

let GetInt = require('./GetInt.js')
let ChangeHardwareVersion = require('./ChangeHardwareVersion.js')
let CloseGripper = require('./CloseGripper.js')
let SetInt = require('./SetInt.js')
let ManageProcess = require('./ManageProcess.js')
let GetWorkspaceRatio = require('./GetWorkspaceRatio.js')
let UpdateConveyorId = require('./UpdateConveyorId.js')
let ControlConveyor = require('./ControlConveyor.js')
let ManagePosition = require('./ManagePosition.js')
let ObjDetection = require('./ObjDetection.js')
let SetString = require('./SetString.js')
let GetDigitalIO = require('./GetDigitalIO.js')
let DebugColorDetection = require('./DebugColorDetection.js')
let GetCalibrationCam = require('./GetCalibrationCam.js')
let PullAirVacuumPump = require('./PullAirVacuumPump.js')
let RobotMove = require('./RobotMove.js')
let ChangeMotorConfig = require('./ChangeMotorConfig.js')
let OpenGripper = require('./OpenGripper.js')
let GetTrajectoryList = require('./GetTrajectoryList.js')
let GetTargetPose = require('./GetTargetPose.js')
let GetSequenceList = require('./GetSequenceList.js')
let PingDxlTool = require('./PingDxlTool.js')
let ManageTrajectory = require('./ManageTrajectory.js')
let SetSequenceAutorun = require('./SetSequenceAutorun.js')
let GetWorkspaceList = require('./GetWorkspaceList.js')
let TakePicture = require('./TakePicture.js')
let SetCalibrationCam = require('./SetCalibrationCam.js')
let DebugMarkers = require('./DebugMarkers.js')
let PushAirVacuumPump = require('./PushAirVacuumPump.js')
let EditGrip = require('./EditGrip.js')
let SetLeds = require('./SetLeds.js')
let SetDigitalIO = require('./SetDigitalIO.js')
let GetPositionList = require('./GetPositionList.js')
let GetWorkspaceRobotPoses = require('./GetWorkspaceRobotPoses.js')
let SendCustomDxlValue = require('./SendCustomDxlValue.js')
let SetConveyor = require('./SetConveyor.js')
let ManageSequence = require('./ManageSequence.js')
let EditWorkspace = require('./EditWorkspace.js')

module.exports = {
  GetInt: GetInt,
  ChangeHardwareVersion: ChangeHardwareVersion,
  CloseGripper: CloseGripper,
  SetInt: SetInt,
  ManageProcess: ManageProcess,
  GetWorkspaceRatio: GetWorkspaceRatio,
  UpdateConveyorId: UpdateConveyorId,
  ControlConveyor: ControlConveyor,
  ManagePosition: ManagePosition,
  ObjDetection: ObjDetection,
  SetString: SetString,
  GetDigitalIO: GetDigitalIO,
  DebugColorDetection: DebugColorDetection,
  GetCalibrationCam: GetCalibrationCam,
  PullAirVacuumPump: PullAirVacuumPump,
  RobotMove: RobotMove,
  ChangeMotorConfig: ChangeMotorConfig,
  OpenGripper: OpenGripper,
  GetTrajectoryList: GetTrajectoryList,
  GetTargetPose: GetTargetPose,
  GetSequenceList: GetSequenceList,
  PingDxlTool: PingDxlTool,
  ManageTrajectory: ManageTrajectory,
  SetSequenceAutorun: SetSequenceAutorun,
  GetWorkspaceList: GetWorkspaceList,
  TakePicture: TakePicture,
  SetCalibrationCam: SetCalibrationCam,
  DebugMarkers: DebugMarkers,
  PushAirVacuumPump: PushAirVacuumPump,
  EditGrip: EditGrip,
  SetLeds: SetLeds,
  SetDigitalIO: SetDigitalIO,
  GetPositionList: GetPositionList,
  GetWorkspaceRobotPoses: GetWorkspaceRobotPoses,
  SendCustomDxlValue: SendCustomDxlValue,
  SetConveyor: SetConveyor,
  ManageSequence: ManageSequence,
  EditWorkspace: EditWorkspace,
};
