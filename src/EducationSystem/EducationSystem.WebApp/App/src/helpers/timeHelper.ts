let normalize = (value: number) => value < 10 ? '0' + value : value

export const sec2time = (sec: number): string => `${normalize(~~((sec % 3600) / 60))}:${normalize(~~sec % 60)}`