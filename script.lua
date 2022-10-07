function get2XwithD (a,b,sqrtD)
    local x1 = (-b+sqrtD)/(2*a)
    local x2 = (-b-sqrtD)/(2*a)
    return x1, x2
end

function getX (a,b)
    local x1 = -b/(2*a)
    return x1
end

function getD (a,b,c)
    return b*b - 4*a*c
end