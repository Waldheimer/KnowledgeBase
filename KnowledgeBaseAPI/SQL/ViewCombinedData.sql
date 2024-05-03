SELECT Descriptors.[ID], CommandText, [System], [Tech], [Lang], [DescriptionText], [Version] from Commands 
JOIN Descriptors 
ON Commands.Descriptor = Descriptors.ID
JOIN Descriptions
ON Descriptors.ID = Descriptions.ID