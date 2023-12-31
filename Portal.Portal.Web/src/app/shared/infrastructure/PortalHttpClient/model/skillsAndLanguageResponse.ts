/**
 * Demo API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { ComputerSkillModel } from './computerSkillModel';
import { EmployeeFileModel } from './employeeFileModel';
import { LanguageModel } from './languageModel';

export interface SkillsAndLanguageResponse { 
    id?: number;
    isMyProfile?: boolean;
    resume?: EmployeeFileModel;
    skills?: Array<ComputerSkillModel>;
    languages?: Array<LanguageModel>;
}