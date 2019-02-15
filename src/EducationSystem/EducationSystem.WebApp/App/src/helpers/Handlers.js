const capitalize = (str = '') => str.slice(0, 1).toUpperCase() + str.slice(1).toLowerCase()

const getInitials = (str = '') => str && `${str.slice(0, 1)}.`

export const getFullName = ({LastName, FirstName, MiddleName}, withInitials) => {
  LastName = capitalize(LastName || '')
  FirstName = capitalize(FirstName || '')
  MiddleName = capitalize(MiddleName || '')

  let fullName = `${LastName} `
  return fullName + (withInitials
      ? `${getInitials(FirstName)} ${getInitials(MiddleName)}`
      : `${FirstName} ${MiddleName}`
  ).trim()
}