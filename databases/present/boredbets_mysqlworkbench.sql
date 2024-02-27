-- MySQL Script generated by MySQL Workbench
-- Thu Feb 22 07:31:12 2024
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema boredbets
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema boredbets
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `boredbets` DEFAULT CHARACTER SET utf8mb4 ;
USE `boredbets` ;

-- -----------------------------------------------------
-- Table `boredbets`.`horses`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `boredbets`.`horses` (
  `id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `name` VARCHAR(255) NULL DEFAULT NULL,
  `age` INT NULL DEFAULT NULL,
  `stallion` TINYINT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;


-- -----------------------------------------------------
-- Table `boredbets`.`jockey`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `boredbets`.`jockey` (
  `id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `name` VARCHAR(255) NULL DEFAULT NULL,
  `quality` INT NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;


-- -----------------------------------------------------
-- Table `boredbets`.`tracks`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `boredbets`.`tracks` (
  `id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `name` VARCHAR(255) NULL DEFAULT NULL,
  `country` VARCHAR(255) NULL DEFAULT NULL,
  `length` FLOAT NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;


-- -----------------------------------------------------
-- Table `boredbets`.`races`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `boredbets`.`races` (
  `id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `race_time` DOUBLE NOT NULL,
  `race_scheduled` DATETIME NOT NULL,
  `weather` VARCHAR(255) NULL DEFAULT NULL,
  `track_id` CHAR(36) CHARACTER SET 'ascii' NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  INDEX `track_id` (`track_id` ASC) VISIBLE,
  CONSTRAINT `races_ibfk_1`
    FOREIGN KEY (`track_id`)
    REFERENCES `boredbets`.`tracks` (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;


-- -----------------------------------------------------
-- Table `boredbets`.`participants`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `boredbets`.`participants` (
  `Id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `race_id` CHAR(36) CHARACTER SET 'ascii' NULL DEFAULT NULL,
  `horse_id` CHAR(36) CHARACTER SET 'ascii' NULL DEFAULT NULL,
  `jockey_id` CHAR(36) CHARACTER SET 'ascii' NULL DEFAULT NULL,
  `placement` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `horse_id` (`horse_id` ASC) VISIBLE,
  INDEX `jockey_id` (`jockey_id` ASC) VISIBLE,
  INDEX `race_id` (`race_id` ASC) VISIBLE,
  CONSTRAINT `participant_ibfk_1`
    FOREIGN KEY (`race_id`)
    REFERENCES `boredbets`.`races` (`id`),
  CONSTRAINT `participant_ibfk_2`
    FOREIGN KEY (`horse_id`)
    REFERENCES `boredbets`.`horses` (`id`),
  CONSTRAINT `participant_ibfk_3`
    FOREIGN KEY (`jockey_id`)
    REFERENCES `boredbets`.`jockey` (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;


-- -----------------------------------------------------
-- Table `boredbets`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `boredbets`.`users` (
  `id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `email` VARCHAR(255) NULL DEFAULT NULL,
  `role` VARCHAR(255) NULL DEFAULT NULL,
  `password` VARCHAR(255) NULL DEFAULT NULL,
  `created` DATETIME(6) NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;


-- -----------------------------------------------------
-- Table `boredbets`.`user_bets`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `boredbets`.`user_bets` (
  `Id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `user_id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `race_id` CHAR(36) CHARACTER SET 'ascii' NULL DEFAULT NULL,
  `horse_id` CHAR(36) CHARACTER SET 'ascii' NULL DEFAULT NULL,
  `bet_amount` FLOAT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_user_bets_user_id` (`user_id` ASC) VISIBLE,
  INDEX `horse_id1` (`horse_id` ASC) VISIBLE,
  INDEX `race_id1` (`race_id` ASC) VISIBLE,
  CONSTRAINT `user_bets_ibfk_1`
    FOREIGN KEY (`race_id`)
    REFERENCES `boredbets`.`races` (`id`),
  CONSTRAINT `user_bets_ibfk_2`
    FOREIGN KEY (`horse_id`)
    REFERENCES `boredbets`.`horses` (`id`),
  CONSTRAINT `user_bets_ibfk_3`
    FOREIGN KEY (`user_id`)
    REFERENCES `boredbets`.`users` (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;


-- -----------------------------------------------------
-- Table `boredbets`.`user_cards`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `boredbets`.`user_cards` (
  `creditcard_num` INT NOT NULL,
  `cvc` INT NOT NULL,
  `exp_date` VARCHAR(255) NOT NULL,
  `card_name` VARCHAR(255) NOT NULL,
  `user_id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  PRIMARY KEY (`creditcard_num`),
  INDEX `IX_user_cards_user_id` (`user_id` ASC) VISIBLE,
  CONSTRAINT `user_cards_ibfk_1`
    FOREIGN KEY (`user_id`)
    REFERENCES `boredbets`.`users` (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;


-- -----------------------------------------------------
-- Table `boredbets`.`user_details`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `boredbets`.`user_details` (
  `user_id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `fullname` VARCHAR(255) NULL DEFAULT NULL,
  `address` VARCHAR(255) NULL DEFAULT NULL,
  `IsPrivate` TINYINT(1) NOT NULL,
  `birth_date` DATETIME(6) NULL DEFAULT NULL,
  PRIMARY KEY (`user_id`),
  CONSTRAINT `user_details_ibfk_1`
    FOREIGN KEY (`user_id`)
    REFERENCES `boredbets`.`users` (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;