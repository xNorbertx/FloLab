declare global {
  interface Date {
    addHours(hours: number): Date;
    addDays(days: number): Date;
    getWeek(): number;
    getDayOfYear(): number;
  }
}

Date.prototype.addHours = function(hours: number): Date {
  if (!hours) {
    return this;
  }

  return new Date(this.getFullYear(), this.getMonth(), this.getDate(), this.getHours() + hours, this.getMinutes());
}

Date.prototype.addDays = function(days: number): Date {
  if (!days) {
    return this;
  }

  return new Date(this.getFullYear(), this.getMonth(), this.getDate() + days, this.getHours(), this.getMinutes());
}

Date.prototype.getWeek = function(): number {
    const msPerDay = 86400000;
    // Set to nearest Thursday: current date + 4 - current day number
    this.setUTCDate(this.getUTCDate() + 4 - (this.getUTCDay()||7));
    // Get first day of year
    const yearStart = new Date(Date.UTC(this.getUTCFullYear(),0,1));
    // Calculate full weeks to nearest Thursday
    return Math.ceil((((this.getTime() - yearStart.getTime()) / msPerDay) + 1)/7);
}

Date.prototype.getDayOfYear = function(): number {
    const msPerDay = 86400000;
    const yearStart = new Date(this.getFullYear(), 0, 1);
    // Calculate full days 
    return Math.ceil((this.getTime() - yearStart.getTime()) / msPerDay);
}

export {}