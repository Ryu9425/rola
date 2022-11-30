/*
 Navicat Premium Data Transfer

 Source Server         : rola
 Source Server Type    : SQLite
 Source Server Version : 3030001
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3030001
 File Encoding         : 65001

 Date: 30/11/2022 11:19:00
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for display
-- ----------------------------
DROP TABLE IF EXISTS "display";
CREATE TABLE "display" (
  "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  "gtadient" TEXT,
  "temperature" TEXT,
  "humidity" TEXT,
  "pressure" TEXT,
  "voltage" TEXT
);

-- ----------------------------
-- Records of display
-- ----------------------------

-- ----------------------------
-- Table structure for main_setting
-- ----------------------------
DROP TABLE IF EXISTS "main_setting";
CREATE TABLE "main_setting" (
  "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  "connection_time" integer,
  "connection_interval" integer,
  "store_url" TEXT,
  "display_count" integer,
  "api_url" TEXT
);

-- ----------------------------
-- Records of main_setting
-- ----------------------------

-- ----------------------------
-- Table structure for sensors
-- ----------------------------
DROP TABLE IF EXISTS "sensors";
CREATE TABLE "sensors" (
  "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  "sensorid" text,
  "uuid" text,
  "data_id" text,
  "data" text,
  "datetime" text
);

-- ----------------------------
-- Records of sensors
-- ----------------------------

-- ----------------------------
-- Table structure for sqlite_sequence
-- ----------------------------
DROP TABLE IF EXISTS "sqlite_sequence";
CREATE TABLE "sqlite_sequence" (
  "name" ,
  "seq" 
);

-- ----------------------------
-- Records of sqlite_sequence
-- ----------------------------
INSERT INTO "sqlite_sequence" VALUES ('sensors', 0);

-- ----------------------------
-- Auto increment value for sensors
-- ----------------------------

PRAGMA foreign_keys = true;
