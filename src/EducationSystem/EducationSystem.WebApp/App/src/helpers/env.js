export default (() => {
  const variablesKey = 'REACT_APP_';
  
  let env = {};
  for (const key in process.env) {
    if (key.startsWith(variablesKey)) {
      const normalizedName = key
        .replace(variablesKey, '')
        .split('_')
        .map(p => `${p[0]}${p.slice(1).toLowerCase()}`)
        .join('');
      
      env[normalizedName] = process.env[key];
    }
  }
  
  return env
})()