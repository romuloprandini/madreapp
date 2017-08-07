-- 
-- Estrutura da tabela `ligacoes` 
-- 
CREATE TABLE `ligacoes` (
  `id` int(11) NOT NULL,
  `telefone` bigint(20) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT NULL,
  `retorno` varchar(1024) DEFAULT NULL,
  `nome` varchar(255) DEFAULT NULL,
  `latitude` double DEFAULT NULL,
  `longitude` double DEFAULT NULL ) ENGINE=InnoDB DEFAULT CHARSET=latin1; 
-- 
-- Indexes for dumped tables
-- 
-- 
-- Indexes for table `ligacoes` 
-- 
ALTER TABLE 
`ligacoes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `IDX_TELEFONE_LIGACOES` (`telefone`); 
-- 
-- AUTO_INCREMENT for dumped tables 
-- 
-- 
-- AUTO_INCREMENT for table `ligacoes` 
-- 
ALTER TABLE `ligacoes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;
