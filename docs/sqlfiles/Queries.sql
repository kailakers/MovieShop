SELECT r.MovieId, m.Title,m.PosterUrl, AVG(r.Rating) AS Rating
			FROM Movie m
				left JOIN Review r ON m.Id = r.MovieId
			GROUP BY r.MovieId, m.Title, m.PosterUrl
		order by AVG(r.Rating) desc


-- EF 3 Generated SQL

SELECT TOP(25) [r].[MovieId] AS [Id], [m].[PosterUrl], [m].[Title], [m].[BackdropUrl], AVG([r].[Rating]) AS [Rating]
      FROM [Review] AS [r]
      INNER JOIN [Movie] AS [m] ON [r].[MovieId] = [m].[Id]
      GROUP BY [r].[MovieId], [m].[PosterUrl], [m].[Title], [m].[BackdropUrl]
      ORDER BY AVG([r].[Rating]) DESC


	SELECT TOP(25) [r].[MovieId] AS [Id], AVG([r].[Rating]) AS [Rating]
      FROM [Review] AS [r]
      GROUP BY [r].[MovieId]
      ORDER BY AVG([r].[Rating]) DESC

		SELECT r.MovieId,
	       AVG(r.Rating)
			FROM [Review] r
			GROUP BY r.MovieId
			ORDER BY AVG(r.Rating) DESC offset 0 rows
		FETCH NEXT 10 rows ONLY;


SELECT  u.Id  
	, u.FirstName
	, CAST(rr.averagerating as decimal(4,2))
      --ROUND( rr.averagerating, 2)
	, rr.reviewcount
			FROM [User] u
				JOIN (
	SELECT r.UserId
		, COUNT(r.UserId) AS reviewcount
		, AVG(r.Rating) AS averagerating
				FROM Review r
				GROUP BY r.UserId
	) AS RR ON u.Id = rr.UserId
			ORDER BY rr.averagerating desc

select  u.Id  
	, u.FirstName 
	, count(r.UserId) as MoviesReviewed
	, AVG(r.Rating) AverageRating
	 FROM [User] u left join Review r
	on u.Id = r.UserId
	group by u.Id, u.FirstName
	order by AverageRating desc