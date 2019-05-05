const capitalize = (str = '') => str.slice(0, 1).toUpperCase() + str.slice(1).toLowerCase()

const getInitials = (str = '') => str && `${str.slice(0, 1)}.`

export const getFullName = (user, withInitials) => {
  if (!user) return '' 
  
  let {LastName, FirstName, MiddleName} = user
  LastName = capitalize(LastName || '')
  FirstName = capitalize(FirstName || '')
  MiddleName = capitalize(MiddleName || '')

  let fullName = `${LastName} `
  return fullName + (withInitials
      ? `${getInitials(FirstName)} ${getInitials(MiddleName)}`
      : `${FirstName} ${MiddleName}`
  ).trim()
}