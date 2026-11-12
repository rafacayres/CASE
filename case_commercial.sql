-- MySQL dump 10.13  Distrib 8.0.46, for Win64 (x86_64)
--
-- Host: localhost    Database: case_commercial
-- ------------------------------------------------------
-- Server version	8.0.46

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `analiseusuario`
--

DROP TABLE IF EXISTS `analiseusuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `analiseusuario` (
  `id_analise` int NOT NULL AUTO_INCREMENT,
  `id_usuario` int NOT NULL,
  `id_analisador` int NOT NULL,
  `decisao` varchar(30) NOT NULL,
  `data_hora` datetime NOT NULL,
  `justificativa` text,
  PRIMARY KEY (`id_analise`),
  KEY `id_usuario` (`id_usuario`),
  KEY `id_analisador` (`id_analisador`),
  CONSTRAINT `analiseusuario_ibfk_1` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `analiseusuario_ibfk_2` FOREIGN KEY (`id_analisador`) REFERENCES `usuario` (`id_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `analiseusuario`
--

LOCK TABLES `analiseusuario` WRITE;
/*!40000 ALTER TABLE `analiseusuario` DISABLE KEYS */;
/*!40000 ALTER TABLE `analiseusuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `apuracao`
--

DROP TABLE IF EXISTS `apuracao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `apuracao` (
  `id_apuracao` int NOT NULL AUTO_INCREMENT,
  `id_jornada` int NOT NULL,
  `valor_informado` decimal(10,2) NOT NULL,
  `data_hora` datetime NOT NULL,
  `id_usuario_responsavel` int NOT NULL,
  `observacao` text,
  `valor_dinheiro` decimal(10,2) DEFAULT NULL,
  `valor_pix` decimal(10,2) DEFAULT NULL,
  `valor_cartao` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id_apuracao`),
  KEY `id_jornada` (`id_jornada`),
  KEY `id_usuario_responsavel` (`id_usuario_responsavel`),
  CONSTRAINT `apuracao_ibfk_1` FOREIGN KEY (`id_jornada`) REFERENCES `jornadasetor` (`id_jornada`),
  CONSTRAINT `apuracao_ibfk_2` FOREIGN KEY (`id_usuario_responsavel`) REFERENCES `usuario` (`id_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `apuracao`
--

LOCK TABLES `apuracao` WRITE;
/*!40000 ALTER TABLE `apuracao` DISABLE KEYS */;
/*!40000 ALTER TABLE `apuracao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `categoria`
--

DROP TABLE IF EXISTS `categoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categoria` (
  `id_categoria` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  PRIMARY KEY (`id_categoria`),
  UNIQUE KEY `nome` (`nome`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categoria`
--

LOCK TABLES `categoria` WRITE;
/*!40000 ALTER TABLE `categoria` DISABLE KEYS */;
/*!40000 ALTER TABLE `categoria` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `despesa`
--

DROP TABLE IF EXISTS `despesa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `despesa` (
  `id_despesa` int NOT NULL AUTO_INCREMENT,
  `id_evento` int NOT NULL,
  `id_fornecedor` int DEFAULT NULL,
  `descricao` varchar(255) NOT NULL,
  `data` date NOT NULL,
  `valor` decimal(10,2) NOT NULL,
  `id_usuario_responsavel` int NOT NULL,
  PRIMARY KEY (`id_despesa`),
  KEY `id_evento` (`id_evento`),
  KEY `id_fornecedor` (`id_fornecedor`),
  KEY `id_usuario_responsavel` (`id_usuario_responsavel`),
  CONSTRAINT `despesa_ibfk_1` FOREIGN KEY (`id_evento`) REFERENCES `evento` (`id_evento`),
  CONSTRAINT `despesa_ibfk_2` FOREIGN KEY (`id_fornecedor`) REFERENCES `fornecedor` (`id_fornecedor`),
  CONSTRAINT `despesa_ibfk_3` FOREIGN KEY (`id_usuario_responsavel`) REFERENCES `usuario` (`id_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `despesa`
--

LOCK TABLES `despesa` WRITE;
/*!40000 ALTER TABLE `despesa` DISABLE KEYS */;
/*!40000 ALTER TABLE `despesa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `entradaitem`
--

DROP TABLE IF EXISTS `entradaitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `entradaitem` (
  `id_entrada_item` int NOT NULL AUTO_INCREMENT,
  `id_entrada` int NOT NULL,
  `id_produto` int NOT NULL,
  `quantidade` decimal(10,3) NOT NULL,
  `custo_unitario` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id_entrada_item`),
  KEY `id_entrada` (`id_entrada`),
  KEY `id_produto` (`id_produto`),
  CONSTRAINT `entradaitem_ibfk_1` FOREIGN KEY (`id_entrada`) REFERENCES `entradamercadoria` (`id_entrada`),
  CONSTRAINT `entradaitem_ibfk_2` FOREIGN KEY (`id_produto`) REFERENCES `produto` (`id_produto`),
  CONSTRAINT `chk_entradaitem_quantidade` CHECK (((`quantidade` > 0) and (`quantidade` = floor(`quantidade`))))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `entradaitem`
--

LOCK TABLES `entradaitem` WRITE;
/*!40000 ALTER TABLE `entradaitem` DISABLE KEYS */;
/*!40000 ALTER TABLE `entradaitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `entradamercadoria`
--

DROP TABLE IF EXISTS `entradamercadoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `entradamercadoria` (
  `id_entrada` int NOT NULL AUTO_INCREMENT,
  `id_fornecedor` int DEFAULT NULL,
  `id_local_destino` int NOT NULL,
  `id_usuario_responsavel` int NOT NULL,
  `tipo_entrada` varchar(30) NOT NULL,
  `data_hora` datetime NOT NULL,
  `numero_documento` varchar(50) DEFAULT NULL,
  `observacao` text,
  `chave_operacao` char(36) DEFAULT NULL,
  `criado_em` datetime DEFAULT NULL,
  `id_usuario_alteracao` int DEFAULT NULL,
  `atualizado_em` datetime DEFAULT NULL,
  `versao` int unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_entrada`),
  UNIQUE KEY `uq_entrada_chave_operacao` (`chave_operacao`),
  KEY `id_fornecedor` (`id_fornecedor`),
  KEY `id_local_destino` (`id_local_destino`),
  KEY `id_usuario_responsavel` (`id_usuario_responsavel`),
  KEY `ix_entrada_usuario_alteracao` (`id_usuario_alteracao`),
  CONSTRAINT `entradamercadoria_ibfk_1` FOREIGN KEY (`id_fornecedor`) REFERENCES `fornecedor` (`id_fornecedor`),
  CONSTRAINT `entradamercadoria_ibfk_2` FOREIGN KEY (`id_local_destino`) REFERENCES `localestoque` (`id_local_estoque`),
  CONSTRAINT `entradamercadoria_ibfk_3` FOREIGN KEY (`id_usuario_responsavel`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `fk_entrada_usuario_alteracao` FOREIGN KEY (`id_usuario_alteracao`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `chk_entradamercadoria_tipo` CHECK ((lower(`tipo_entrada`) in (_cp850'compra',_cp850'doacao')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `entradamercadoria`
--

LOCK TABLES `entradamercadoria` WRITE;
/*!40000 ALTER TABLE `entradamercadoria` DISABLE KEYS */;
/*!40000 ALTER TABLE `entradamercadoria` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `equipeevento`
--

DROP TABLE IF EXISTS `equipeevento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `equipeevento` (
  `id_equipe` int NOT NULL AUTO_INCREMENT,
  `id_evento` int NOT NULL,
  `id_pessoa` int NOT NULL,
  `id_perfil` int NOT NULL,
  `ativo` tinyint(1) NOT NULL,
  `id_usuario_criacao` int DEFAULT NULL,
  `criado_em` datetime DEFAULT NULL,
  `id_usuario_alteracao` int DEFAULT NULL,
  `atualizado_em` datetime DEFAULT NULL,
  `versao` int unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_equipe`),
  UNIQUE KEY `id_evento` (`id_evento`,`id_pessoa`),
  KEY `id_pessoa` (`id_pessoa`),
  KEY `id_perfil` (`id_perfil`),
  KEY `ix_equipeevento_usuario_criacao` (`id_usuario_criacao`),
  KEY `ix_equipeevento_usuario_alteracao` (`id_usuario_alteracao`),
  CONSTRAINT `equipeevento_ibfk_1` FOREIGN KEY (`id_evento`) REFERENCES `evento` (`id_evento`),
  CONSTRAINT `equipeevento_ibfk_2` FOREIGN KEY (`id_pessoa`) REFERENCES `pessoa` (`id_pessoa`),
  CONSTRAINT `equipeevento_ibfk_3` FOREIGN KEY (`id_perfil`) REFERENCES `perfil` (`id_perfil`),
  CONSTRAINT `fk_equipeevento_usuario_alteracao` FOREIGN KEY (`id_usuario_alteracao`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `fk_equipeevento_usuario_criacao` FOREIGN KEY (`id_usuario_criacao`) REFERENCES `usuario` (`id_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipeevento`
--

LOCK TABLES `equipeevento` WRITE;
/*!40000 ALTER TABLE `equipeevento` DISABLE KEYS */;
/*!40000 ALTER TABLE `equipeevento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `escala`
--

DROP TABLE IF EXISTS `escala`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `escala` (
  `id_escala` int NOT NULL AUTO_INCREMENT,
  `id_equipe` int NOT NULL,
  `id_jornada` int NOT NULL,
  `entrada_prevista` time NOT NULL,
  `saida_prevista` time NOT NULL,
  `situacao` varchar(30) NOT NULL DEFAULT 'pendente',
  PRIMARY KEY (`id_escala`),
  UNIQUE KEY `id_equipe` (`id_equipe`,`id_jornada`),
  KEY `id_jornada` (`id_jornada`),
  CONSTRAINT `escala_ibfk_1` FOREIGN KEY (`id_equipe`) REFERENCES `equipeevento` (`id_equipe`),
  CONSTRAINT `escala_ibfk_2` FOREIGN KEY (`id_jornada`) REFERENCES `jornadasetor` (`id_jornada`),
  CONSTRAINT `chk_escala_situacao` CHECK ((lower(`situacao`) in (_cp850'pendente',_cp850'confirmada',_cp850'cancelada')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `escala`
--

LOCK TABLES `escala` WRITE;
/*!40000 ALTER TABLE `escala` DISABLE KEYS */;
/*!40000 ALTER TABLE `escala` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estoque`
--

DROP TABLE IF EXISTS `estoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `estoque` (
  `id_estoque` int NOT NULL AUTO_INCREMENT,
  `id_local_estoque` int NOT NULL,
  `id_produto` int NOT NULL,
  `quantidade_minima` decimal(10,3) NOT NULL,
  `quantidade_ideal` decimal(10,3) NOT NULL,
  `quantidade_atual` decimal(18,3) NOT NULL DEFAULT '0.000',
  PRIMARY KEY (`id_estoque`),
  UNIQUE KEY `id_local_estoque` (`id_local_estoque`,`id_produto`),
  KEY `id_produto` (`id_produto`),
  CONSTRAINT `estoque_ibfk_1` FOREIGN KEY (`id_local_estoque`) REFERENCES `localestoque` (`id_local_estoque`),
  CONSTRAINT `estoque_ibfk_2` FOREIGN KEY (`id_produto`) REFERENCES `produto` (`id_produto`),
  CONSTRAINT `chk_estoque_quantidades` CHECK (((`quantidade_minima` >= 0) and (`quantidade_ideal` >= `quantidade_minima`) and (`quantidade_atual` >= 0) and (`quantidade_minima` = floor(`quantidade_minima`)) and (`quantidade_ideal` = floor(`quantidade_ideal`)) and (`quantidade_atual` = floor(`quantidade_atual`))))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estoque`
--

LOCK TABLES `estoque` WRITE;
/*!40000 ALTER TABLE `estoque` DISABLE KEYS */;
/*!40000 ALTER TABLE `estoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estoquesetor`
--

DROP TABLE IF EXISTS `estoquesetor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `estoquesetor` (
  `id_setor` int NOT NULL,
  `id_local_estoque` int NOT NULL,
  `id_evento` int NOT NULL,
  PRIMARY KEY (`id_setor`),
  UNIQUE KEY `uq_estoquesetor_local` (`id_local_estoque`),
  KEY `fk_estoquesetor_setor_evento` (`id_setor`,`id_evento`),
  KEY `fk_estoquesetor_local_evento` (`id_local_estoque`,`id_evento`),
  CONSTRAINT `estoquesetor_ibfk_1` FOREIGN KEY (`id_setor`) REFERENCES `setor` (`id_setor`),
  CONSTRAINT `estoquesetor_ibfk_2` FOREIGN KEY (`id_local_estoque`) REFERENCES `localestoque` (`id_local_estoque`),
  CONSTRAINT `fk_estoquesetor_local_evento` FOREIGN KEY (`id_local_estoque`, `id_evento`) REFERENCES `localestoque` (`id_local_estoque`, `id_evento`),
  CONSTRAINT `fk_estoquesetor_setor_evento` FOREIGN KEY (`id_setor`, `id_evento`) REFERENCES `setor` (`id_setor`, `id_evento`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estoquesetor`
--

LOCK TABLES `estoquesetor` WRITE;
/*!40000 ALTER TABLE `estoquesetor` DISABLE KEYS */;
/*!40000 ALTER TABLE `estoquesetor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `evento`
--

DROP TABLE IF EXISTS `evento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `evento` (
  `id_evento` int NOT NULL AUTO_INCREMENT,
  `id_instituicao` int NOT NULL,
  `id_tipo_evento` int NOT NULL,
  `nome` varchar(150) NOT NULL,
  `local` varchar(200) NOT NULL,
  `data_inicio` date NOT NULL,
  `data_fim` date NOT NULL,
  `id_responsavel` int NOT NULL,
  `situacao` varchar(30) NOT NULL DEFAULT 'planejado',
  `observacao` text,
  `id_usuario_criacao` int DEFAULT NULL,
  `criado_em` datetime DEFAULT NULL,
  `versao` int unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_evento`),
  UNIQUE KEY `uq_evento_id_instituicao` (`id_evento`,`id_instituicao`),
  KEY `id_instituicao` (`id_instituicao`),
  KEY `id_tipo_evento` (`id_tipo_evento`),
  KEY `id_responsavel` (`id_responsavel`),
  KEY `ix_evento_usuario_criacao` (`id_usuario_criacao`),
  CONSTRAINT `evento_ibfk_1` FOREIGN KEY (`id_instituicao`) REFERENCES `instituicao` (`id_instituicao`),
  CONSTRAINT `evento_ibfk_2` FOREIGN KEY (`id_tipo_evento`) REFERENCES `tipoevento` (`id_tipo_evento`),
  CONSTRAINT `evento_ibfk_3` FOREIGN KEY (`id_responsavel`) REFERENCES `pessoa` (`id_pessoa`),
  CONSTRAINT `fk_evento_usuario_criacao` FOREIGN KEY (`id_usuario_criacao`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `chk_evento_situacao` CHECK ((lower(`situacao`) in (_cp850'planejado',_cp850'em_andamento',_cp850'finalizado',_cp850'cancelado')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `evento`
--

LOCK TABLES `evento` WRITE;
/*!40000 ALTER TABLE `evento` DISABLE KEYS */;
/*!40000 ALTER TABLE `evento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fornecedor`
--

DROP TABLE IF EXISTS `fornecedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fornecedor` (
  `id_fornecedor` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(150) NOT NULL,
  `documento` varchar(18) DEFAULT NULL,
  `telefone` varchar(20) DEFAULT NULL,
  `email` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`id_fornecedor`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fornecedor`
--

LOCK TABLES `fornecedor` WRITE;
/*!40000 ALTER TABLE `fornecedor` DISABLE KEYS */;
INSERT INTO `fornecedor` VALUES (2,'RAFAEL',NULL,'11937392939','cayresrafa@gmail.com');
/*!40000 ALTER TABLE `fornecedor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `historicoalteracaooperacao`
--

DROP TABLE IF EXISTS `historicoalteracaooperacao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `historicoalteracaooperacao` (
  `id_historico` bigint unsigned NOT NULL AUTO_INCREMENT,
  `entidade` varchar(30) NOT NULL,
  `id_entidade` int NOT NULL,
  `id_usuario` int NOT NULL,
  `data_hora` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `justificativa` varchar(500) DEFAULT NULL,
  `dados_anteriores` json NOT NULL,
  `dados_novos` json NOT NULL,
  PRIMARY KEY (`id_historico`),
  KEY `ix_historico_entidade` (`entidade`,`id_entidade`,`data_hora`),
  KEY `ix_historico_usuario` (`id_usuario`,`data_hora`),
  CONSTRAINT `fk_historico_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `chk_historico_entidade` CHECK ((`entidade` in (_utf8mb4'evento',_utf8mb4'equipeevento',_utf8mb4'solicitacao',_utf8mb4'solicitacaoestoquecentral',_utf8mb4'entradamercadoria',_utf8mb4'transferenciaestoque',_utf8mb4'registro')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `historicoalteracaooperacao`
--

LOCK TABLES `historicoalteracaooperacao` WRITE;
/*!40000 ALTER TABLE `historicoalteracaooperacao` DISABLE KEYS */;
/*!40000 ALTER TABLE `historicoalteracaooperacao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `instituicao`
--

DROP TABLE IF EXISTS `instituicao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `instituicao` (
  `id_instituicao` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(150) NOT NULL,
  `documento` varchar(18) DEFAULT NULL,
  `telefone` varchar(20) DEFAULT NULL,
  `email` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`id_instituicao`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `instituicao`
--

LOCK TABLES `instituicao` WRITE;
/*!40000 ALTER TABLE `instituicao` DISABLE KEYS */;
/*!40000 ALTER TABLE `instituicao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jornadasetor`
--

DROP TABLE IF EXISTS `jornadasetor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jornadasetor` (
  `id_jornada` int NOT NULL AUTO_INCREMENT,
  `id_setor` int NOT NULL,
  `data` date NOT NULL,
  `id_turno` int NOT NULL,
  PRIMARY KEY (`id_jornada`),
  UNIQUE KEY `id_setor` (`id_setor`,`data`,`id_turno`),
  KEY `id_turno` (`id_turno`),
  CONSTRAINT `jornadasetor_ibfk_1` FOREIGN KEY (`id_setor`) REFERENCES `setor` (`id_setor`),
  CONSTRAINT `jornadasetor_ibfk_2` FOREIGN KEY (`id_turno`) REFERENCES `turno` (`id_turno`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jornadasetor`
--

LOCK TABLES `jornadasetor` WRITE;
/*!40000 ALTER TABLE `jornadasetor` DISABLE KEYS */;
/*!40000 ALTER TABLE `jornadasetor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `localestoque`
--

DROP TABLE IF EXISTS `localestoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `localestoque` (
  `id_local_estoque` int NOT NULL AUTO_INCREMENT,
  `id_instituicao` int NOT NULL,
  `nome` varchar(100) NOT NULL,
  `ativo` tinyint(1) NOT NULL,
  `eh_central` tinyint(1) NOT NULL,
  `eh_geral_evento` tinyint(1) NOT NULL DEFAULT '0',
  `id_evento` int DEFAULT NULL,
  `id_central_unico` tinyint GENERATED ALWAYS AS ((case when (`eh_central` = 1) then 1 else NULL end)) STORED,
  `id_estoque_geral_unico` tinyint GENERATED ALWAYS AS ((case when (`eh_geral_evento` = 1) then 1 else NULL end)) STORED,
  PRIMARY KEY (`id_local_estoque`),
  UNIQUE KEY `uq_localestoque_id_instituicao` (`id_local_estoque`,`id_instituicao`),
  UNIQUE KEY `uq_localestoque_central` (`id_instituicao`,`id_central_unico`),
  UNIQUE KEY `uq_localestoque_evento` (`id_local_estoque`,`id_evento`),
  UNIQUE KEY `uq_localestoque_nome_evento` (`id_instituicao`,`id_evento`,`nome`),
  UNIQUE KEY `uq_localestoque_geral_evento` (`id_evento`,`id_estoque_geral_unico`),
  KEY `ix_localestoque_evento_instituicao` (`id_evento`,`id_instituicao`),
  CONSTRAINT `fk_localestoque_evento_instituicao` FOREIGN KEY (`id_evento`, `id_instituicao`) REFERENCES `evento` (`id_evento`, `id_instituicao`),
  CONSTRAINT `localestoque_ibfk_1` FOREIGN KEY (`id_instituicao`) REFERENCES `instituicao` (`id_instituicao`),
  CONSTRAINT `chk_localestoque_classificacao` CHECK ((((`eh_central` = 1) and (`eh_geral_evento` = 0) and (`id_evento` is null)) or ((`eh_central` = 0) and (`id_evento` is not null) and (`eh_geral_evento` in (0,1)))))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `localestoque`
--

LOCK TABLES `localestoque` WRITE;
/*!40000 ALTER TABLE `localestoque` DISABLE KEYS */;
/*!40000 ALTER TABLE `localestoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `perfil`
--

DROP TABLE IF EXISTS `perfil`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `perfil` (
  `id_perfil` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  PRIMARY KEY (`id_perfil`),
  UNIQUE KEY `uq_perfil_nome` (`nome`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `perfil`
--

LOCK TABLES `perfil` WRITE;
/*!40000 ALTER TABLE `perfil` DISABLE KEYS */;
/*!40000 ALTER TABLE `perfil` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `perfilsistema`
--

DROP TABLE IF EXISTS `perfilsistema`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `perfilsistema` (
  `id_perfil_sistema` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  PRIMARY KEY (`id_perfil_sistema`),
  UNIQUE KEY `uq_perfilsistema_nome` (`nome`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `perfilsistema`
--

LOCK TABLES `perfilsistema` WRITE;
/*!40000 ALTER TABLE `perfilsistema` DISABLE KEYS */;
INSERT INTO `perfilsistema` VALUES (1,'Administrador'),(2,'Gerente'),(3,'Membro');
/*!40000 ALTER TABLE `perfilsistema` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pessoa`
--

DROP TABLE IF EXISTS `pessoa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pessoa` (
  `id_pessoa` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `telefone` varchar(20) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `foto_perfil` mediumblob,
  PRIMARY KEY (`id_pessoa`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pessoa`
--

LOCK TABLES `pessoa` WRITE;
/*!40000 ALTER TABLE `pessoa` DISABLE KEYS */;
INSERT INTO `pessoa` VALUES (1,'Administrador CASE',NULL,'cayresrafa@gmail.com',NULL);
/*!40000 ALTER TABLE `pessoa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produto`
--

DROP TABLE IF EXISTS `produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `produto` (
  `id_produto` int NOT NULL AUTO_INCREMENT,
  `id_categoria` int NOT NULL,
  `nome` varchar(100) NOT NULL,
  `descricao` text,
  `unidade_medida` varchar(20) NOT NULL DEFAULT 'UN',
  `ativo` tinyint(1) NOT NULL,
  `imagem` mediumblob,
  PRIMARY KEY (`id_produto`),
  KEY `id_categoria` (`id_categoria`),
  CONSTRAINT `produto_ibfk_1` FOREIGN KEY (`id_categoria`) REFERENCES `categoria` (`id_categoria`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produto`
--

LOCK TABLES `produto` WRITE;
/*!40000 ALTER TABLE `produto` DISABLE KEYS */;
/*!40000 ALTER TABLE `produto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produtofornecedor`
--

DROP TABLE IF EXISTS `produtofornecedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `produtofornecedor` (
  `id_produto` int NOT NULL,
  `id_fornecedor` int NOT NULL,
  `codigo_no_fornecedor` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id_produto`,`id_fornecedor`),
  KEY `id_fornecedor` (`id_fornecedor`),
  CONSTRAINT `produtofornecedor_ibfk_1` FOREIGN KEY (`id_produto`) REFERENCES `produto` (`id_produto`),
  CONSTRAINT `produtofornecedor_ibfk_2` FOREIGN KEY (`id_fornecedor`) REFERENCES `fornecedor` (`id_fornecedor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produtofornecedor`
--

LOCK TABLES `produtofornecedor` WRITE;
/*!40000 ALTER TABLE `produtofornecedor` DISABLE KEYS */;
/*!40000 ALTER TABLE `produtofornecedor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produtosetor`
--

DROP TABLE IF EXISTS `produtosetor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `produtosetor` (
  `id_produto_setor` int NOT NULL AUTO_INCREMENT,
  `id_setor` int NOT NULL,
  `id_produto` int NOT NULL,
  `preco_venda` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id_produto_setor`),
  UNIQUE KEY `id_setor` (`id_setor`,`id_produto`),
  KEY `id_produto` (`id_produto`),
  CONSTRAINT `produtosetor_ibfk_1` FOREIGN KEY (`id_setor`) REFERENCES `setor` (`id_setor`),
  CONSTRAINT `produtosetor_ibfk_2` FOREIGN KEY (`id_produto`) REFERENCES `produto` (`id_produto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produtosetor`
--

LOCK TABLES `produtosetor` WRITE;
/*!40000 ALTER TABLE `produtosetor` DISABLE KEYS */;
/*!40000 ALTER TABLE `produtosetor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `registro`
--

DROP TABLE IF EXISTS `registro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `registro` (
  `id_registro` int NOT NULL AUTO_INCREMENT,
  `id_escala` int NOT NULL,
  `id_usuario_responsavel` int NOT NULL,
  `tipo` varchar(20) NOT NULL,
  `data_hora` datetime NOT NULL,
  `observacao` text,
  `chave_operacao` char(36) DEFAULT NULL,
  `criado_em` datetime DEFAULT NULL,
  `id_usuario_alteracao` int DEFAULT NULL,
  `atualizado_em` datetime DEFAULT NULL,
  `versao` int unsigned NOT NULL DEFAULT '1',
  `forma_pagamento` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id_registro`),
  UNIQUE KEY `uq_registro_chave_operacao` (`chave_operacao`),
  KEY `id_escala` (`id_escala`),
  KEY `id_usuario_responsavel` (`id_usuario_responsavel`),
  KEY `ix_registro_usuario_alteracao` (`id_usuario_alteracao`),
  CONSTRAINT `fk_registro_usuario_alteracao` FOREIGN KEY (`id_usuario_alteracao`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `registro_ibfk_1` FOREIGN KEY (`id_escala`) REFERENCES `escala` (`id_escala`),
  CONSTRAINT `registro_ibfk_2` FOREIGN KEY (`id_usuario_responsavel`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `chk_registro_tipo` CHECK ((lower(`tipo`) in (_utf8mb4'venda',_utf8mb4'perda')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `registro`
--

LOCK TABLES `registro` WRITE;
/*!40000 ALTER TABLE `registro` DISABLE KEYS */;
/*!40000 ALTER TABLE `registro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `registroitem`
--

DROP TABLE IF EXISTS `registroitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `registroitem` (
  `id_registro_item` int NOT NULL AUTO_INCREMENT,
  `id_registro` int NOT NULL,
  `id_produto_setor` int NOT NULL,
  `quantidade` decimal(10,3) NOT NULL,
  `preco_unitario_praticado` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id_registro_item`),
  KEY `id_registro` (`id_registro`),
  KEY `id_produto_setor` (`id_produto_setor`),
  CONSTRAINT `registroitem_ibfk_1` FOREIGN KEY (`id_registro`) REFERENCES `registro` (`id_registro`),
  CONSTRAINT `registroitem_ibfk_2` FOREIGN KEY (`id_produto_setor`) REFERENCES `produtosetor` (`id_produto_setor`),
  CONSTRAINT `chk_registroitem_quantidade` CHECK (((`quantidade` > 0) and (`quantidade` = floor(`quantidade`))))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `registroitem`
--

LOCK TABLES `registroitem` WRITE;
/*!40000 ALTER TABLE `registroitem` DISABLE KEYS */;
/*!40000 ALTER TABLE `registroitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `setor`
--

DROP TABLE IF EXISTS `setor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `setor` (
  `id_setor` int NOT NULL AUTO_INCREMENT,
  `id_evento` int NOT NULL,
  `nome` varchar(100) NOT NULL,
  PRIMARY KEY (`id_setor`),
  UNIQUE KEY `id_evento` (`id_evento`,`nome`),
  UNIQUE KEY `uq_setor_id_evento` (`id_setor`,`id_evento`),
  CONSTRAINT `setor_ibfk_1` FOREIGN KEY (`id_evento`) REFERENCES `evento` (`id_evento`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `setor`
--

LOCK TABLES `setor` WRITE;
/*!40000 ALTER TABLE `setor` DISABLE KEYS */;
/*!40000 ALTER TABLE `setor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `solicitacao`
--

DROP TABLE IF EXISTS `solicitacao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `solicitacao` (
  `id_solicitacao` int NOT NULL AUTO_INCREMENT,
  `id_escala` int NOT NULL,
  `tipo` varchar(30) NOT NULL,
  `descricao` text,
  `data_hora` datetime NOT NULL,
  `situacao` varchar(30) NOT NULL DEFAULT 'pendente',
  `id_usuario_solicitante` int DEFAULT NULL,
  `id_usuario_decisor` int DEFAULT NULL,
  `decidida_em` datetime DEFAULT NULL,
  `criado_em` datetime DEFAULT NULL,
  `versao` int unsigned NOT NULL DEFAULT '1',
  `justificativa_rejeicao` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id_solicitacao`),
  KEY `id_escala` (`id_escala`),
  KEY `ix_solicitacao_solicitante` (`id_usuario_solicitante`),
  KEY `ix_solicitacao_decisor` (`id_usuario_decisor`),
  CONSTRAINT `fk_solicitacao_decisor` FOREIGN KEY (`id_usuario_decisor`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `fk_solicitacao_solicitante` FOREIGN KEY (`id_usuario_solicitante`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `solicitacao_ibfk_1` FOREIGN KEY (`id_escala`) REFERENCES `escala` (`id_escala`),
  CONSTRAINT `chk_solicitacao_decisao_preenchida` CHECK ((((`id_usuario_decisor` is null) and (`decidida_em` is null)) or ((`id_usuario_decisor` is not null) and (`decidida_em` is not null)))),
  CONSTRAINT `chk_solicitacao_justificativa_rejeicao` CHECK (((`justificativa_rejeicao` is null) or (char_length(trim(`justificativa_rejeicao`)) > 0))),
  CONSTRAINT `chk_solicitacao_situacao` CHECK ((lower(`situacao`) in (_utf8mb4'pendente',_utf8mb4'aprovada',_utf8mb4'atendida',_utf8mb4'rejeitada',_utf8mb4'cancelada')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `solicitacao`
--

LOCK TABLES `solicitacao` WRITE;
/*!40000 ALTER TABLE `solicitacao` DISABLE KEYS */;
/*!40000 ALTER TABLE `solicitacao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `solicitacaoestoquecentral`
--

DROP TABLE IF EXISTS `solicitacaoestoquecentral`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `solicitacaoestoquecentral` (
  `id_solicitacao_estoque_central` int NOT NULL AUTO_INCREMENT,
  `tipo` varchar(20) NOT NULL,
  `id_instituicao` int NOT NULL,
  `id_evento` int NOT NULL,
  `id_local_origem` int NOT NULL,
  `id_local_destino` int NOT NULL,
  `id_usuario_solicitante` int NOT NULL,
  `data_hora` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `situacao` varchar(30) NOT NULL DEFAULT 'pendente',
  `id_usuario_decisor` int DEFAULT NULL,
  `decidida_em` datetime DEFAULT NULL,
  `justificativa_rejeicao` varchar(500) DEFAULT NULL,
  `versao` int unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_solicitacao_estoque_central`),
  KEY `ix_solicitacao_central_pendencia` (`situacao`,`data_hora`),
  KEY `ix_solicitacao_central_solicitante` (`id_usuario_solicitante`,`data_hora`),
  KEY `ix_solicitacao_central_decisor` (`id_usuario_decisor`),
  KEY `fk_solicitacao_central_evento_instituicao` (`id_evento`,`id_instituicao`),
  KEY `fk_solicitacao_central_origem_instituicao` (`id_local_origem`,`id_instituicao`),
  KEY `fk_solicitacao_central_destino_instituicao` (`id_local_destino`,`id_instituicao`),
  CONSTRAINT `fk_solicitacao_central_decisor` FOREIGN KEY (`id_usuario_decisor`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `fk_solicitacao_central_destino_instituicao` FOREIGN KEY (`id_local_destino`, `id_instituicao`) REFERENCES `localestoque` (`id_local_estoque`, `id_instituicao`),
  CONSTRAINT `fk_solicitacao_central_evento_instituicao` FOREIGN KEY (`id_evento`, `id_instituicao`) REFERENCES `evento` (`id_evento`, `id_instituicao`),
  CONSTRAINT `fk_solicitacao_central_origem_instituicao` FOREIGN KEY (`id_local_origem`, `id_instituicao`) REFERENCES `localestoque` (`id_local_estoque`, `id_instituicao`),
  CONSTRAINT `fk_solicitacao_central_solicitante` FOREIGN KEY (`id_usuario_solicitante`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `chk_solicitacao_central_decisao` CHECK ((((`situacao` = _utf8mb4'pendente') and (`id_usuario_decisor` is null) and (`decidida_em` is null)) or ((`situacao` in (_utf8mb4'aprovada',_utf8mb4'rejeitada',_utf8mb4'atendida')) and (`id_usuario_decisor` is not null) and (`decidida_em` is not null)))),
  CONSTRAINT `chk_solicitacao_central_justificativa` CHECK ((((`situacao` = _utf8mb4'rejeitada') and (`justificativa_rejeicao` is not null) and (char_length(trim(`justificativa_rejeicao`)) > 0)) or ((`situacao` <> _utf8mb4'rejeitada') and (`justificativa_rejeicao` is null)))),
  CONSTRAINT `chk_solicitacao_central_locais` CHECK ((`id_local_origem` <> `id_local_destino`)),
  CONSTRAINT `chk_solicitacao_central_situacao` CHECK ((`situacao` in (_utf8mb4'pendente',_utf8mb4'aprovada',_utf8mb4'rejeitada',_utf8mb4'atendida'))),
  CONSTRAINT `chk_solicitacao_central_tipo` CHECK ((`tipo` in (_utf8mb4'liberacao',_utf8mb4'devolucao')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `solicitacaoestoquecentral`
--

LOCK TABLES `solicitacaoestoquecentral` WRITE;
/*!40000 ALTER TABLE `solicitacaoestoquecentral` DISABLE KEYS */;
/*!40000 ALTER TABLE `solicitacaoestoquecentral` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `solicitacaoestoquecentralitem`
--

DROP TABLE IF EXISTS `solicitacaoestoquecentralitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `solicitacaoestoquecentralitem` (
  `id_solicitacao_estoque_central_item` int NOT NULL AUTO_INCREMENT,
  `id_solicitacao_estoque_central` int NOT NULL,
  `id_produto` int NOT NULL,
  `quantidade` decimal(10,3) NOT NULL,
  PRIMARY KEY (`id_solicitacao_estoque_central_item`),
  UNIQUE KEY `uq_solicitacao_central_item_produto` (`id_solicitacao_estoque_central`,`id_produto`),
  KEY `ix_solicitacao_central_item_produto` (`id_produto`),
  CONSTRAINT `fk_solicitacao_central_item_cabecalho` FOREIGN KEY (`id_solicitacao_estoque_central`) REFERENCES `solicitacaoestoquecentral` (`id_solicitacao_estoque_central`),
  CONSTRAINT `fk_solicitacao_central_item_produto` FOREIGN KEY (`id_produto`) REFERENCES `produto` (`id_produto`),
  CONSTRAINT `chk_solicitacao_central_item_quantidade` CHECK (((`quantidade` > 0) and (`quantidade` = floor(`quantidade`))))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `solicitacaoestoquecentralitem`
--

LOCK TABLES `solicitacaoestoquecentralitem` WRITE;
/*!40000 ALTER TABLE `solicitacaoestoquecentralitem` DISABLE KEYS */;
/*!40000 ALTER TABLE `solicitacaoestoquecentralitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `solicitacaoitem`
--

DROP TABLE IF EXISTS `solicitacaoitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `solicitacaoitem` (
  `id_solicitacao_item` int NOT NULL AUTO_INCREMENT,
  `id_solicitacao` int NOT NULL,
  `id_produto_setor` int NOT NULL,
  `quantidade` decimal(10,3) NOT NULL,
  PRIMARY KEY (`id_solicitacao_item`),
  KEY `id_solicitacao` (`id_solicitacao`),
  KEY `id_produto_setor` (`id_produto_setor`),
  CONSTRAINT `solicitacaoitem_ibfk_1` FOREIGN KEY (`id_solicitacao`) REFERENCES `solicitacao` (`id_solicitacao`),
  CONSTRAINT `solicitacaoitem_ibfk_2` FOREIGN KEY (`id_produto_setor`) REFERENCES `produtosetor` (`id_produto_setor`),
  CONSTRAINT `chk_solicitacaoitem_quantidade` CHECK (((`quantidade` > 0) and (`quantidade` = floor(`quantidade`))))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `solicitacaoitem`
--

LOCK TABLES `solicitacaoitem` WRITE;
/*!40000 ALTER TABLE `solicitacaoitem` DISABLE KEYS */;
/*!40000 ALTER TABLE `solicitacaoitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipoevento`
--

DROP TABLE IF EXISTS `tipoevento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipoevento` (
  `id_tipo_evento` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  PRIMARY KEY (`id_tipo_evento`),
  UNIQUE KEY `nome` (`nome`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipoevento`
--

LOCK TABLES `tipoevento` WRITE;
/*!40000 ALTER TABLE `tipoevento` DISABLE KEYS */;
/*!40000 ALTER TABLE `tipoevento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transferenciaestoque`
--

DROP TABLE IF EXISTS `transferenciaestoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transferenciaestoque` (
  `id_transferencia` int NOT NULL AUTO_INCREMENT,
  `id_local_origem` int NOT NULL,
  `id_local_destino` int NOT NULL,
  `id_solicitacao` int DEFAULT NULL,
  `id_usuario_responsavel` int NOT NULL,
  `data_hora` datetime NOT NULL,
  `observacao` text,
  `chave_operacao` char(36) DEFAULT NULL,
  `criado_em` datetime DEFAULT NULL,
  `id_usuario_alteracao` int DEFAULT NULL,
  `atualizado_em` datetime DEFAULT NULL,
  `versao` int unsigned NOT NULL DEFAULT '1',
  `id_solicitacao_estoque_central` int DEFAULT NULL,
  PRIMARY KEY (`id_transferencia`),
  UNIQUE KEY `uq_transferencia_solicitacao` (`id_solicitacao`),
  UNIQUE KEY `uq_transferencia_chave_operacao` (`chave_operacao`),
  UNIQUE KEY `uq_transferencia_solicitacao_central` (`id_solicitacao_estoque_central`),
  KEY `id_local_origem` (`id_local_origem`),
  KEY `id_local_destino` (`id_local_destino`),
  KEY `id_usuario_responsavel` (`id_usuario_responsavel`),
  KEY `ix_transferencia_usuario_alteracao` (`id_usuario_alteracao`),
  CONSTRAINT `fk_transferencia_solicitacao_central` FOREIGN KEY (`id_solicitacao_estoque_central`) REFERENCES `solicitacaoestoquecentral` (`id_solicitacao_estoque_central`),
  CONSTRAINT `fk_transferencia_usuario_alteracao` FOREIGN KEY (`id_usuario_alteracao`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `transferenciaestoque_ibfk_1` FOREIGN KEY (`id_local_origem`) REFERENCES `localestoque` (`id_local_estoque`),
  CONSTRAINT `transferenciaestoque_ibfk_2` FOREIGN KEY (`id_local_destino`) REFERENCES `localestoque` (`id_local_estoque`),
  CONSTRAINT `transferenciaestoque_ibfk_3` FOREIGN KEY (`id_solicitacao`) REFERENCES `solicitacao` (`id_solicitacao`),
  CONSTRAINT `transferenciaestoque_ibfk_4` FOREIGN KEY (`id_usuario_responsavel`) REFERENCES `usuario` (`id_usuario`),
  CONSTRAINT `transferenciaestoque_chk_1` CHECK ((`id_local_origem` <> `id_local_destino`))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transferenciaestoque`
--

LOCK TABLES `transferenciaestoque` WRITE;
/*!40000 ALTER TABLE `transferenciaestoque` DISABLE KEYS */;
/*!40000 ALTER TABLE `transferenciaestoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transferenciaitem`
--

DROP TABLE IF EXISTS `transferenciaitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transferenciaitem` (
  `id_transferencia_item` int NOT NULL AUTO_INCREMENT,
  `id_transferencia` int NOT NULL,
  `id_produto` int NOT NULL,
  `quantidade` decimal(10,3) NOT NULL,
  PRIMARY KEY (`id_transferencia_item`),
  UNIQUE KEY `id_transferencia` (`id_transferencia`,`id_produto`),
  KEY `id_produto` (`id_produto`),
  CONSTRAINT `transferenciaitem_ibfk_1` FOREIGN KEY (`id_transferencia`) REFERENCES `transferenciaestoque` (`id_transferencia`),
  CONSTRAINT `transferenciaitem_ibfk_2` FOREIGN KEY (`id_produto`) REFERENCES `produto` (`id_produto`),
  CONSTRAINT `chk_transferenciaitem_quantidade` CHECK (((`quantidade` > 0) and (`quantidade` = floor(`quantidade`))))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transferenciaitem`
--

LOCK TABLES `transferenciaitem` WRITE;
/*!40000 ALTER TABLE `transferenciaitem` DISABLE KEYS */;
/*!40000 ALTER TABLE `transferenciaitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `turno`
--

DROP TABLE IF EXISTS `turno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `turno` (
  `id_turno` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) NOT NULL,
  PRIMARY KEY (`id_turno`),
  UNIQUE KEY `nome` (`nome`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turno`
--

LOCK TABLES `turno` WRITE;
/*!40000 ALTER TABLE `turno` DISABLE KEYS */;
/*!40000 ALTER TABLE `turno` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `id_usuario` int NOT NULL AUTO_INCREMENT,
  `id_pessoa` int NOT NULL,
  `id_perfil_sistema` int DEFAULT NULL,
  `login` varchar(50) NOT NULL,
  `senha_hash` varchar(255) NOT NULL,
  `situacao_conta` varchar(30) NOT NULL DEFAULT 'pendente',
  `solicitado_em` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_usuario`),
  UNIQUE KEY `id_pessoa` (`id_pessoa`),
  UNIQUE KEY `login` (`login`),
  KEY `id_perfil_sistema` (`id_perfil_sistema`),
  CONSTRAINT `usuario_ibfk_1` FOREIGN KEY (`id_pessoa`) REFERENCES `pessoa` (`id_pessoa`),
  CONSTRAINT `usuario_ibfk_2` FOREIGN KEY (`id_perfil_sistema`) REFERENCES `perfilsistema` (`id_perfil_sistema`),
  CONSTRAINT `chk_usuario_aprovado_com_perfil` CHECK (((lower(`situacao_conta`) <> _cp850'aprovado') or (`id_perfil_sistema` is not null))),
  CONSTRAINT `chk_usuario_situacao` CHECK ((lower(`situacao_conta`) in (_cp850'pendente',_cp850'aprovado',_cp850'rejeitado')))
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,1,1,'admin','REDEFINIR_HASH_LOCALMENTE','aprovado','2026-11-12 15:13:06');
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-11-12 18:27:01
