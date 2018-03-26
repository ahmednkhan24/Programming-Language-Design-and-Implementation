-- part 4
 Select Area, Total = Count(*)
 From Crimes
 Where Area > 0
 Group by Area
 order by Area asc

-- part 3
Select Year, Total = Count(*)
From Crimes
Where Area IN (Select Area 
               From Areas 
               Where AreaName = 'Loop')
Group by Year
Order by Year Desc


-- part 2
Select Year, Total = Count(*), Arrested = Sum(Convert(Float, Arrested))
from Crimes
group by Year
Order by Year Desc


-- part 1
SELECT Year, Total = (Count(*))
FROM Crimes
GROUP BY Year
ORDER BY Year Desc