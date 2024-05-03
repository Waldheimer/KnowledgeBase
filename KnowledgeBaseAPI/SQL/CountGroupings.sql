-- Show the Count of all Commands/Snippets/Documentations Grouped by the Combination
-- of System,Tech,Lang

SELECT COUNT(ID) as count, [System],[Tech],[Lang]
  FROM [Descriptors]
  GROUP BY [System],[Tech],[Lang]

-- Show the Count of only the Command-related Descriptors
SELECT COUNT(ID) as count, [System],[Tech],[Lang]
  FROM [Descriptors]
  WHERE [Descriptors].ID IN (SELECT [ID] FROM [Commands])
  GROUP BY [System],[Tech],[Lang]

-- Combine the Counts of Commands/Snippets/Documentations
SELECT
(SELECT COUNT(Commands.Descriptor) FROM [Commands]) AS '# Commands' ,
(SELECT COUNT(Snippets.Descriptor) FROM [Snippets]) AS '# Snippets',
(SELECT COUNT(Documentations.Descriptor) FROM [Documentations]) AS '# Documentations'

-- Create a View with all Counts
CREATE VIEW Counts AS
SELECT
(SELECT COUNT(Commands.Descriptor) FROM [Commands]) AS '# Commands' ,
(SELECT COUNT(Snippets.Descriptor) FROM [Snippets]) AS '# Snippets',
(SELECT COUNT(Documentations.Descriptor) FROM [Documentations]) AS '# Documentations',
(SELECT COUNT(Distinct [System]) FROM [Descriptors]) AS '# Systems',
(SELECT COUNT(Distinct [Tech]) FROM [Descriptors]) AS '# Technologies',
(SELECT COUNT(Distinct [Lang]) FROM [Descriptors]) AS '# Languages'


-- Create View with only all distinct Systems
CREATE VIEW All_Systems AS
SELECT Distinct [System] AS 'Systems' FROM [Descriptors]
-- Create View with only all distinct Languages
CREATE VIEW All_Technologies AS
SELECT Distinct Tech AS 'Technologies' FROM [Descriptors]
-- Create View with only all distinct Languages
CREATE VIEW All_Languages AS
SELECT Distinct [Lang] AS 'Languages' FROM [Descriptors]