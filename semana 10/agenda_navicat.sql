/*
 Navicat Premium Data Transfer

 Source Server         : Local
 Source Server Type    : MySQL
 Source Server Version : 50621
 Source Host           : localhost:3306
 Source Schema         : agenda

 Target Server Type    : MySQL
 Target Server Version : 50621
 File Encoding         : 65001

 Date: 02/10/2022 17:19:33
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for contactos
-- ----------------------------
DROP TABLE IF EXISTS `contactos`;
CREATE TABLE `contactos`  (
  `codigo` int(8) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(75) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `clave` varchar(75) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `fecha` varchar(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `nivel` varchar(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  PRIMARY KEY (`codigo`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of contactos
-- ----------------------------
INSERT INTO `contactos` VALUES (1, 'user1', 'user1', '02/06/1993', '1');
INSERT INTO `contactos` VALUES (3, 'USER3', 'USER3', '07/09/1990', '1');
INSERT INTO `contactos` VALUES (4, 'user4', 'user4', '05/10/1995', '1');
INSERT INTO `contactos` VALUES (5, 'USER6', 'USER6', '07/10/1991', '2');
INSERT INTO `contactos` VALUES (6, 'USER6', 'USER6', '07/05/1989', '1');
INSERT INTO `contactos` VALUES (7, 'PRUEBA', 'PRUEBA', '12/10/1998', '1');
INSERT INTO `contactos` VALUES (8, '11', '11', '15/12/1990', '1');
INSERT INTO `contactos` VALUES (9, 'PEDRO', 'PEDRO', '12/12/1990', '1');

SET FOREIGN_KEY_CHECKS = 1;
